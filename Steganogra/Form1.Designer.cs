namespace Steganogra
{
    partial class OknoGlowne
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pasekMenu = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wczytajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.LSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opisProgramuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opisMetodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasekStatusu = new System.Windows.Forms.StatusStrip();
            this.infoToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.oknoWczytywania = new System.Windows.Forms.OpenFileDialog();
            this.oknoZapisywania = new System.Windows.Forms.SaveFileDialog();
            this.ukryjButton = new System.Windows.Forms.Button();
            this.oknoWiadomosci = new System.Windows.Forms.RichTextBox();
            this.odczytajButton = new System.Windows.Forms.Button();
            this.iloscZnakowLabel = new System.Windows.Forms.Label();
            this.nazwaPlikuLabel = new System.Windows.Forms.Label();
            this.zamknijPlikToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasekMenu.SuspendLayout();
            this.pasekStatusu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pasekMenu
            // 
            this.pasekMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.tesToolStripMenuItem1,
            this.pomocToolStripMenuItem});
            this.pasekMenu.Location = new System.Drawing.Point(0, 0);
            this.pasekMenu.Name = "pasekMenu";
            this.pasekMenu.Size = new System.Drawing.Size(376, 24);
            this.pasekMenu.TabIndex = 0;
            this.pasekMenu.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wczytajToolStripMenuItem,
            this.zapiszToolStripMenuItem,
            this.zamknijPlikToolStripMenuItem2,
            this.zamknijToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // wczytajToolStripMenuItem
            // 
            this.wczytajToolStripMenuItem.Name = "wczytajToolStripMenuItem";
            this.wczytajToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wczytajToolStripMenuItem.Text = "Wczytaj";
            this.wczytajToolStripMenuItem.Click += new System.EventHandler(this.wczytajToolStripMenuItem_Click);
            // 
            // zapiszToolStripMenuItem
            // 
            this.zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            this.zapiszToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zapiszToolStripMenuItem.Text = "Zapisz";
            this.zapiszToolStripMenuItem.Click += new System.EventHandler(this.zapiszToolStripMenuItem_Click);
            // 
            // zamknijToolStripMenuItem
            // 
            this.zamknijToolStripMenuItem.Name = "zamknijToolStripMenuItem";
            this.zamknijToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zamknijToolStripMenuItem.Text = "Zakończ";
            this.zamknijToolStripMenuItem.Click += new System.EventHandler(this.zamknijToolStripMenuItem_Click);
            // 
            // tesToolStripMenuItem1
            // 
            this.tesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LSBToolStripMenuItem,
            this.FFTToolStripMenuItem});
            this.tesToolStripMenuItem1.Name = "tesToolStripMenuItem1";
            this.tesToolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.tesToolStripMenuItem1.Text = "Metoda";
            this.tesToolStripMenuItem1.CheckedChanged += new System.EventHandler(this.tesToolStripMenuItem1_CheckedChanged);
            // 
            // LSBToolStripMenuItem
            // 
            this.LSBToolStripMenuItem.Checked = true;
            this.LSBToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LSBToolStripMenuItem.Name = "LSBToolStripMenuItem";
            this.LSBToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.LSBToolStripMenuItem.Text = "LSB";
            this.LSBToolStripMenuItem.CheckedChanged += new System.EventHandler(this.tesToolStripMenuItem1_CheckedChanged);
            this.LSBToolStripMenuItem.Click += new System.EventHandler(this.LSBToolStripMenuItem_Click);
            // 
            // FFTToolStripMenuItem
            // 
            this.FFTToolStripMenuItem.Name = "FFTToolStripMenuItem";
            this.FFTToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.FFTToolStripMenuItem.Text = "FFT";
            this.FFTToolStripMenuItem.CheckedChanged += new System.EventHandler(this.tesToolStripMenuItem1_CheckedChanged);
            this.FFTToolStripMenuItem.Click += new System.EventHandler(this.FFTToolStripMenuItem_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opisProgramuToolStripMenuItem,
            this.opisMetodToolStripMenuItem,
            this.toolStripMenuItem1});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // opisProgramuToolStripMenuItem
            // 
            this.opisProgramuToolStripMenuItem.Name = "opisProgramuToolStripMenuItem";
            this.opisProgramuToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.opisProgramuToolStripMenuItem.Text = "Opis programu";
            this.opisProgramuToolStripMenuItem.Click += new System.EventHandler(this.opisProgramuToolStripMenuItem_Click);
            // 
            // opisMetodToolStripMenuItem
            // 
            this.opisMetodToolStripMenuItem.Name = "opisMetodToolStripMenuItem";
            this.opisMetodToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.opisMetodToolStripMenuItem.Text = "Opis metod";
            this.opisMetodToolStripMenuItem.Click += new System.EventHandler(this.opisMetodToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem1.Text = "Instrukcja obsługi";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // pasekStatusu
            // 
            this.pasekStatusu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripStatusLabel});
            this.pasekStatusu.Location = new System.Drawing.Point(0, 221);
            this.pasekStatusu.Name = "pasekStatusu";
            this.pasekStatusu.Size = new System.Drawing.Size(376, 22);
            this.pasekStatusu.TabIndex = 1;
            this.pasekStatusu.Text = "statusStrip1";
            // 
            // infoToolStripStatusLabel
            // 
            this.infoToolStripStatusLabel.Name = "infoToolStripStatusLabel";
            this.infoToolStripStatusLabel.Size = new System.Drawing.Size(64, 17);
            this.infoToolStripStatusLabel.Text = "Wczytaj plik";
            // 
            // oknoWczytywania
            // 
            this.oknoWczytywania.DefaultExt = "*.wav";
            this.oknoWczytywania.FileName = "plik_wejsciowy.wav";
            this.oknoWczytywania.Filter = "Wave File (*.wav)|*.wav";
            // 
            // oknoZapisywania
            // 
            this.oknoZapisywania.DefaultExt = "*.wav";
            this.oknoZapisywania.FileName = "plik_wyjsciowy.wav";
            this.oknoZapisywania.Filter = "Wave File (*.wav)|*.wav";
            // 
            // ukryjButton
            // 
            this.ukryjButton.Location = new System.Drawing.Point(28, 75);
            this.ukryjButton.Name = "ukryjButton";
            this.ukryjButton.Size = new System.Drawing.Size(131, 23);
            this.ukryjButton.TabIndex = 2;
            this.ukryjButton.Text = "Ukryj wiadomość";
            this.ukryjButton.UseVisualStyleBackColor = true;
            this.ukryjButton.Click += new System.EventHandler(this.ukryj_Click);
            this.ukryjButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ukryj_MouseDown);
            // 
            // oknoWiadomosci
            // 
            this.oknoWiadomosci.Location = new System.Drawing.Point(189, 75);
            this.oknoWiadomosci.Name = "oknoWiadomosci";
            this.oknoWiadomosci.Size = new System.Drawing.Size(175, 115);
            this.oknoWiadomosci.TabIndex = 4;
            this.oknoWiadomosci.Text = "";
            // 
            // odczytajButton
            // 
            this.odczytajButton.Location = new System.Drawing.Point(28, 167);
            this.odczytajButton.Name = "odczytajButton";
            this.odczytajButton.Size = new System.Drawing.Size(131, 23);
            this.odczytajButton.TabIndex = 5;
            this.odczytajButton.Text = "Odczytaj wiadomość";
            this.odczytajButton.UseVisualStyleBackColor = true;
            this.odczytajButton.Click += new System.EventHandler(this.odczytaj_Click);
            this.odczytajButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.odczytaj_MouseDown);
            // 
            // iloscZnakowLabel
            // 
            this.iloscZnakowLabel.AutoSize = true;
            this.iloscZnakowLabel.Location = new System.Drawing.Point(37, 117);
            this.iloscZnakowLabel.Name = "iloscZnakowLabel";
            this.iloscZnakowLabel.Size = new System.Drawing.Size(0, 13);
            this.iloscZnakowLabel.TabIndex = 6;
            this.iloscZnakowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nazwaPlikuLabel
            // 
            this.nazwaPlikuLabel.AutoSize = true;
            this.nazwaPlikuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nazwaPlikuLabel.Location = new System.Drawing.Point(222, 45);
            this.nazwaPlikuLabel.Name = "nazwaPlikuLabel";
            this.nazwaPlikuLabel.Size = new System.Drawing.Size(108, 13);
            this.nazwaPlikuLabel.TabIndex = 7;
            this.nazwaPlikuLabel.Text = "Nie wybrano pliku";
            // 
            // zamknijPlikToolStripMenuItem2
            // 
            this.zamknijPlikToolStripMenuItem2.Name = "zamknijPlikToolStripMenuItem2";
            this.zamknijPlikToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.zamknijPlikToolStripMenuItem2.Text = "Zakończ (plik)";
            this.zamknijPlikToolStripMenuItem2.Click += new System.EventHandler(this.zamknijPlikToolStripMenuItem2_Click);
            // 
            // OknoGlowne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(376, 243);
            this.Controls.Add(this.nazwaPlikuLabel);
            this.Controls.Add(this.iloscZnakowLabel);
            this.Controls.Add(this.odczytajButton);
            this.Controls.Add(this.oknoWiadomosci);
            this.Controls.Add(this.ukryjButton);
            this.Controls.Add(this.pasekStatusu);
            this.Controls.Add(this.pasekMenu);
            this.MainMenuStrip = this.pasekMenu;
            this.MaximumSize = new System.Drawing.Size(384, 270);
            this.MinimumSize = new System.Drawing.Size(384, 270);
            this.Name = "OknoGlowne";
            this.Text = "Steganografia";
            this.pasekMenu.ResumeLayout(false);
            this.pasekMenu.PerformLayout();
            this.pasekStatusu.ResumeLayout(false);
            this.pasekStatusu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip pasekMenu;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wczytajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszToolStripMenuItem;
        private System.Windows.Forms.StatusStrip pasekStatusu;
        private System.Windows.Forms.ToolStripStatusLabel infoToolStripStatusLabel;
        private System.Windows.Forms.OpenFileDialog oknoWczytywania;
        private System.Windows.Forms.SaveFileDialog oknoZapisywania;
        private System.Windows.Forms.Button ukryjButton;
        private System.Windows.Forms.ToolStripMenuItem tesToolStripMenuItem1;
        private System.Windows.Forms.RichTextBox oknoWiadomosci;
        private System.Windows.Forms.ToolStripMenuItem LSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opisProgramuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opisMetodToolStripMenuItem;
        private System.Windows.Forms.Button odczytajButton;
        private System.Windows.Forms.Label iloscZnakowLabel;
        private System.Windows.Forms.ToolStripMenuItem zamknijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label nazwaPlikuLabel;
        private System.Windows.Forms.ToolStripMenuItem zamknijPlikToolStripMenuItem2;
    }
}

