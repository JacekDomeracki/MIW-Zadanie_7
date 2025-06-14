namespace Zadanie_7
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxEkran = new System.Windows.Forms.TextBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.textBoxLicznik = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxParK = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxMet = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(905, 51);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(114, 31);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // textBoxEkran
            // 
            this.textBoxEkran.Location = new System.Drawing.Point(175, 26);
            this.textBoxEkran.Multiline = true;
            this.textBoxEkran.Name = "textBoxEkran";
            this.textBoxEkran.ReadOnly = true;
            this.textBoxEkran.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxEkran.Size = new System.Drawing.Size(698, 589);
            this.textBoxEkran.TabIndex = 1;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(905, 156);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(114, 31);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // textBoxLicznik
            // 
            this.textBoxLicznik.Location = new System.Drawing.Point(905, 108);
            this.textBoxLicznik.Name = "textBoxLicznik";
            this.textBoxLicznik.ReadOnly = true;
            this.textBoxLicznik.Size = new System.Drawing.Size(114, 23);
            this.textBoxLicznik.TabIndex = 3;
            this.textBoxLicznik.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Parametr K";
            // 
            // textBoxParK
            // 
            this.textBoxParK.Location = new System.Drawing.Point(29, 83);
            this.textBoxParK.Name = "textBoxParK";
            this.textBoxParK.Size = new System.Drawing.Size(114, 23);
            this.textBoxParK.TabIndex = 12;
            this.textBoxParK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxParK.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxParK_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Metryka";
            // 
            // comboBoxMet
            // 
            this.comboBoxMet.FormattingEnabled = true;
            this.comboBoxMet.Location = new System.Drawing.Point(29, 164);
            this.comboBoxMet.Name = "comboBoxMet";
            this.comboBoxMet.Size = new System.Drawing.Size(121, 23);
            this.comboBoxMet.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 641);
            this.Controls.Add(this.comboBoxMet);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxParK);
            this.Controls.Add(this.textBoxLicznik);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.textBoxEkran);
            this.Controls.Add(this.buttonStart);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Algorytm K-NN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonStart;
        private TextBox textBoxEkran;
        private Button buttonReset;
        private TextBox textBoxLicznik;
        private Label label5;
        private TextBox textBoxParK;
        private Label label6;
        private ComboBox comboBoxMet;
    }
}