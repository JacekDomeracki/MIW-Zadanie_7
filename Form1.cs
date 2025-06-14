///algorytm k-nn | Jacek Domeracki | numer albumu: 173518

using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zadanie_7
{
    public partial class Form1 : Form
    {
        string bazaProbekDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string bazaProbekFile = "Baza_probek_Iris.txt";

        List<List<double>> Baza_probek_wzorcowych = new List<List<double>>();
        List<List<double>> Klasy_probek = new List<List<double>>();             //unikatowe klasy + suma metryk z testowym

        int ROZMIAR;            //w³aœciwy rozmiar próbek i jednoczeœnie pozycja klasy w próbce
        int ILE_PROBEK;
        int ILE_KLAS;

        List<List<double>> Baza_probek_znormalizowana = new List<List<double>>();
        List<List<double>> Obliczone_metryki_probek = new List<List<double>>();         //niezmienna klasa + oryginalny indeks + metryka z testowym
        
        List<int> Wybrane_indeksy = new List<int>();
        List<double> Wybrane_wartosci = new List<double>();

        List<int> Wyniki_analizy_probek = new List<int>();                      // -1:nieustalony, 0:z³y, 1:dobry
        List<List<int>> Wyniki_zapamietane = new List<List<int>>();
        List<int> Wyniki_bazowe = new List<int> { -1, 0, 1 };

        const int CO_ILE_LICZ = 5;         //co ile iteracji pokazywaæ licznik i przetestowane próbki

        //wartoœci domyœlne
        int PAR_K = 3;
        int MET_DOM = 1;        //Euklidesowa

        delegate double Metryka(List<double> A, List<double> B, int Rozmiar);

        StringBuilder buforTekstu = new StringBuilder();

        public Form1()
        {
            InitializeComponent();

            var methods = typeof(Funkcje).GetMethods().Where(m => m.ReturnType == typeof(double)
                            && m.GetParameters().Length == 3 && m.GetParameters()[0].ParameterType == typeof(List<double>)).ToArray();

            textBoxParK.Text = PAR_K.ToString();
            comboBoxMet.DataSource = methods;
            comboBoxMet.DisplayMember = "Name";
            comboBoxMet.SelectedIndex = MET_DOM;

            Funkcje.Pobierz_liste_probek(Path.Combine(bazaProbekDirectory, bazaProbekFile), ref Baza_probek_wzorcowych);

            ROZMIAR = Baza_probek_wzorcowych[0].Count - 1;
            ILE_PROBEK = Baza_probek_wzorcowych.Count;

            Klasy_probek.Add(Baza_probek_wzorcowych.GroupBy(x => x[ROZMIAR]).Select(x => x.First())     //wiersz z unikatowymi klasami
                            .Select(x => x[ROZMIAR]).OrderBy(x => x).ToList());
            Klasy_probek.Add(Enumerable.Repeat(0.0, Klasy_probek[0].Count).ToList());                   //wiersz z sumami metryk
            ILE_KLAS = Klasy_probek[0].Count;

            Baza_probek_znormalizowana = Baza_probek_wzorcowych[0]                              //transpozycja
                            .Select((_, i) => Baza_probek_wzorcowych.Select(x => x[i]).ToList()).ToList();

            for (int i = 0; i < ROZMIAR; i++)
            {
                double Min = Baza_probek_znormalizowana[i].Min();
                double Max = Baza_probek_znormalizowana[i].Max();
                if (Min == Max) continue;

                Baza_probek_znormalizowana[i] = Baza_probek_znormalizowana[i]                   //normalizacja
                                .Select(x => Math.Round((x - Min) / (Max - Min), 4)).ToList();
            }
            Baza_probek_znormalizowana = Baza_probek_znormalizowana[0]                          //transpozycja powrotna
                            .Select((_, i) => Baza_probek_znormalizowana.Select(x => x[i]).ToList()).ToList();

            Obliczone_metryki_probek.Add(Baza_probek_wzorcowych.Select(x => x[ROZMIAR]).ToList());              //wiersz z klasami
            Obliczone_metryki_probek.Add(Enumerable.Range(0, ILE_PROBEK).Select(x => (double)x).ToList());      //wiersz z indeksami    (nie bêd¹ potrzebne)
            Obliczone_metryki_probek.Add(Enumerable.Repeat(0.0, ILE_PROBEK).ToList());                          //wiersz z metrykami

            ButtonReset_Click(null, null);
        }

        private void ButtonReset_Click(object? sender, EventArgs? e)
        {
            buttonReset.Visible = false;
            textBoxLicznik.Text = 0.ToString();
            textBoxParK.Enabled = true;
            comboBoxMet.Enabled = true;

            textBoxEkran.Text = "ALGORYTM  K - NN" + Environment.NewLine + Environment.NewLine;
            if (Wyniki_zapamietane.Count > 0)
            {
                textBoxEkran.AppendText("/\\/\\------>  HISTORIA" + Environment.NewLine);
                for (int i = 0; i < Wyniki_zapamietane.Count; i++)
                {
                    string s = "" + comboBoxMet.Items[Wyniki_zapamietane[i][1]].ToString();
                    textBoxEkran.AppendText(String.Format("------>  PARAM. K : {0,4}  |  METRYKA : {1,15}  |  SKUTECZNOŒÆ KLASYF. : {2,6:F2} %  |  SKUTECZ. BEZ NU. : {3,6:F2} %",
                            Wyniki_zapamietane[i][0], s.Substring(7, s.IndexOf("(") - 7), (double)Wyniki_zapamietane[i][4] / ILE_PROBEK * 100,
                            ILE_PROBEK > Wyniki_zapamietane[i][2] ? (double)Wyniki_zapamietane[i][4] / (ILE_PROBEK - Wyniki_zapamietane[i][2]) * 100 : 0)
                            + Environment.NewLine);
                }
                textBoxEkran.AppendText(Environment.NewLine);
            }

            buttonStart.Enabled = true;
            buttonStart.Focus();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            textBoxParK.Enabled = false;
            comboBoxMet.Enabled = false;

            PAR_K = int.Parse(textBoxParK.Text);
            Metryka met = (Metryka)Delegate.CreateDelegate(typeof(Metryka), (System.Reflection.MethodInfo)comboBoxMet.SelectedItem);

            textBoxEkran.AppendText(String.Format("/\\/\\------>") + Environment.NewLine);
            textBoxEkran.AppendText("-->  START" + Environment.NewLine + Environment.NewLine);
            buforTekstu.Clear();

            Wyniki_analizy_probek = Enumerable.Repeat(-1, ILE_PROBEK).ToList();

            for (int i = 0; i < ILE_PROBEK; i++)
            {
                Obliczone_metryki_probek[2] = Enumerable.Repeat(0.0, ILE_PROBEK).ToList();
                Obliczone_metryki_probek[2][i] = -1.0;                  //niekonieczne
                Klasy_probek[1] = Enumerable.Repeat(0.0, ILE_KLAS).ToList();

                for (int j = 0; j < ILE_PROBEK; j++)
                {
                    if (i == j) continue;

                    Obliczone_metryki_probek[2][j] = met(Baza_probek_znormalizowana[i], Baza_probek_znormalizowana[j], ROZMIAR);
                }

                for (int j = 0; j < ILE_KLAS; j++)
                {
                    Wybrane_indeksy = Obliczone_metryki_probek[0].Select((x, i) => (x, i)).Where(xi => xi.x == Klasy_probek[0][j]).Select(xi => xi.i).ToList();
                    Wybrane_indeksy.Remove(i);

                    if (Wybrane_indeksy.Count < PAR_K)
                    {
                        Klasy_probek[1][j] = -1;
                    }
                    else
                    {
                        Wybrane_wartosci = Obliczone_metryki_probek[2].Where((_, i) => Wybrane_indeksy.Contains(i)).OrderBy(x => x).ToList();

                        Klasy_probek[1][j] = Wybrane_wartosci.Take(PAR_K).Sum();
                    }
                }

                int jmin = -1;
                for (int j = 0; j < ILE_KLAS; j++)
                {
                    if (Klasy_probek[1][j] > -1)
                    {
                        jmin = j;
                        break;
                    }
                }
                if (jmin >= 0)
                {
                    for (int j = jmin + 1; j < ILE_KLAS; j++)
                    {
                        if (Klasy_probek[1][j] < Klasy_probek[1][jmin])
                        {
                            jmin = j;
                        }
                    }
                    for (int j = 0; j < ILE_KLAS; j++)
                    {
                        if (j == jmin) continue;

                        if(Klasy_probek[1][j] == Klasy_probek[1][jmin])
                        {
                            jmin = -1;
                            break;
                        }
                    }
                }

                if (jmin >= 0)
                {
                    Wyniki_analizy_probek[i] = Baza_probek_znormalizowana[i][ROZMIAR] == Klasy_probek[0][jmin] ? 1 : 0;
                }

                buforTekstu.AppendLine(String.Format("------>  PRÓBKA NR : {0,8}  |  KLASA : {1,6}  |  KLASA ALG. : {2,6}  |  STATUS: {3,15}",
                                i + 1, Baza_probek_znormalizowana[i][ROZMIAR], jmin >= 0 ? Klasy_probek[0][Math.Abs(jmin)] : -1,
                                (Wyniki_analizy_probek[i]) switch { -1 => "Nieustalony", 0 => "Z³y", 1 => "Dobry" }));

                if ((i + 1) % CO_ILE_LICZ == 0)
                {
                    textBoxLicznik.Text = (i + 1).ToString("#,##0");
                    Application.DoEvents();

                    textBoxEkran.AppendText(buforTekstu.ToString());
                    buforTekstu.Clear();
                }
            }

            textBoxEkran.AppendText(Environment.NewLine + "-->  KONIEC" + Environment.NewLine);
            textBoxLicznik.Text = ILE_PROBEK.ToString("#,##0");

            textBoxEkran.AppendText(String.Format("/\\/\\------>  PODSUMOWANIE") + Environment.NewLine + Environment.NewLine);

            List<int> nowy = new List<int> { PAR_K, comboBoxMet.SelectedIndex };
            Wyniki_zapamietane.Add(nowy.Concat(Wyniki_analizy_probek.Concat(Wyniki_bazowe).GroupBy(x => x).OrderBy(x => x.First()).Select(x => x.Count() - 1)).ToList());

            string s = "" + comboBoxMet.Items[Wyniki_zapamietane.Last()[1]].ToString();
            textBoxEkran.AppendText(String.Format("------>  PARAM. K : {0,4}  |  METRYKA : {1,15}  |  NIEUSTALONE : {2,4}  |  Z£E : {3,4}  |  DOBRE : {4,4}",
                    Wyniki_zapamietane.Last()[0], s.Substring(7, s.IndexOf("(") - 7),
                    Wyniki_zapamietane.Last()[2], Wyniki_zapamietane.Last()[3], Wyniki_zapamietane.Last()[4])
                    + Environment.NewLine);

            textBoxEkran.AppendText(String.Format("------>                                    |  SKUTECZNOŒÆ KLASYF. : {0,6:F2} %  |  SKUTECZ. BEZ NU. : {1,6:F2} %",
                    (double)Wyniki_zapamietane.Last()[4] / ILE_PROBEK * 100,
                    ILE_PROBEK > Wyniki_zapamietane.Last()[2] ? (double)Wyniki_zapamietane.Last()[4] / (ILE_PROBEK - Wyniki_zapamietane.Last()[2]) * 100 : 0)
                    + Environment.NewLine);

            buttonReset.Visible = true;
            buttonReset.Focus();
        }

        private void textBoxParK_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!int.TryParse(textBoxParK.Text, out int liczba) || liczba < 1 || liczba > 250)
            {
                MessageBox.Show("Wpisz liczbê ca³kowit¹ >= 1 oraz <= 250");
                e.Cancel = true;
            }
        }
    }
}
