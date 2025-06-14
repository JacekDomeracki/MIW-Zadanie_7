using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zadanie_7
{
    internal static class Funkcje
    {
        public static void Pobierz_liste_probek(string filePath, ref List<List<double>> lista_probek)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split('\t');
                    List<double> row = new List<double>();

                    foreach (var col in columns)
                    {
                        double.TryParse(col, NumberStyles.Any, CultureInfo.InvariantCulture, out double number);
                        row.Add(number);
                    }
                    lista_probek.Add(row);
                }
            }
        }

        public static double Manhattan(List<double> A, List<double> B, int Rozmiar)
        {
            double m = 0;
            for (int i = 0; i < Rozmiar; i++)
            {
                m += Math.Abs(A[i] - B[i]);
            }
            m = Math.Round(m, 4);
            return m;
        }

        public static double Euklidesowa(List<double> A, List<double> B, int Rozmiar)
        {
            double m = 0;
            for (int i = 0; i < Rozmiar; i++)
            {
                m += Math.Pow(A[i] - B[i], 2);
            }
            m = Math.Round(Math.Sqrt(m), 4);
            return m;
        }

        public static double Czebyszewa(List<double> A, List<double> B, int Rozmiar)
        {
            double m = 0;
            for (int i = 0; i < Rozmiar; i++)
            {
                if (m < Math.Abs(A[i] - B[i]))
                {
                    m = Math.Abs(A[i] - B[i]);
                }
            }
            m = Math.Round(m, 4);
            return m;
        }

        public static double p3_Minkowskiego(List<double> A, List<double> B, int Rozmiar)
        {
            int p = 3;
            double m = 0;
            for (int i = 0; i < Rozmiar; i++)
            {
                m += Math.Pow(Math.Abs(A[i] - B[i]), p);
            }
            m = Math.Round(Math.Pow(m, 1 / (double)p), 4);
            return m;
        }

        public static double p4_Minkowskiego(List<double> A, List<double> B, int Rozmiar)
        {
            int p = 4;
            double m = 0;
            for (int i = 0; i < Rozmiar; i++)
            {
                m += Math.Pow(Math.Abs(A[i] - B[i]), p);
            }
            m = Math.Round(Math.Pow(m, 1 / (double)p), 4);
            return m;
        }

        public static double z_Logarytmem(List<double> A, List<double> B, int Rozmiar)
        {
            double m = 0;
            for (int i = 0; i < Rozmiar; i++)
            {
                m += Math.Abs(Math.Log10(A[i]) - Math.Log10(B[i]));
            }
            m = Math.Round(m, 4);
            return m;
        }
    }
}
