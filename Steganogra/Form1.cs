using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge.Math;

namespace Steganogra
{
    public partial class OknoGlowne : Form
    {
        FileStream fs;                              // strumień danych
        BinaryReader br;                            // odczyt binarny 
        BinaryWriter bw;                            // zapis binarny
        WavHeader Header = new WavHeader();         // nagłówek pliku
        List<short> lDataList;                      // lewy kanał danych odczytanych
        List<short> rDataList;                      // prawy kanał danych odczytanych
        List<short> lNewDataList;                   // lewy kanał danych zapisywanych
        List<short> rNewDataList;                   // prawy kanał danych zapisywanych
        string messageS = "";                       // ukryta wiadomość (string)
        byte[] messageB;                            // ukryta wiadomosc (byte[])
        byte layer = 3;                             // warstwa ukrywania
        string filePath = "";                       // ścieżka pliku
        string fileName = "";                       // nazwa pliku
        int max = 2048;                             // rozmiar paczki do transformaty
        bool programFile = false;                   // czy w programie wczytano już plik        
        int mml = 0;                                // max. długość wiadomości (obliczana)
        const int maxLen = 16384;                   // max. długośc wiadomości (stała)
  
        public OknoGlowne()
        {
            InitializeComponent();
        }

        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oknoWczytywania.ShowDialog() == DialogResult.OK)
            {
                filePath = oknoWczytywania.FileName;
                fileName = oknoWczytywania.SafeFileName;  
                try
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    Header.riffID = br.ReadBytes(4);
                    Header.size = br.ReadUInt32();
                    Header.wavID = br.ReadBytes(4);
                    Header.fmtID = br.ReadBytes(4);
                    Header.fmtSize = br.ReadUInt32();
                    Header.format = br.ReadUInt16();
                    Header.channels = br.ReadUInt16(); 
                    Header.sampleRate = br.ReadUInt32();
                    Header.bytePerSec = br.ReadUInt32();
                    Header.blockSize = br.ReadUInt16();
                    Header.bit = br.ReadUInt16();
                    Header.dataID = br.ReadBytes(4);
                    Header.dataSize = br.ReadUInt32();
                    lDataList = new List<short>();
                    rDataList = new List<short>(); 
                    for (int i = 0; i < Header.dataSize / Header.blockSize; i++)
                    {
                        lDataList.Add((short)br.ReadInt16());
                        rDataList.Add((short)br.ReadInt16());     
                    }
                    lNewDataList = new List<short>(lDataList);
                    rNewDataList = new List<short>(rDataList);
                    infoToolStripStatusLabel.Text = "Wczytano plik: " + fileName;
                    nazwaPlikuLabel.Text =  fileName;
                    oknoWiadomosci.Text = "";
                    programFile = true;
                    if (lDataList.Count > 0)
                    {
                        if (LSBToolStripMenuItem.Checked)
                            mml = Math.Min(lDataList.Count, maxLen) / 8;
                        else
                            mml = Math.Min(lDataList.Count / max, maxLen) / 8;
                        iloscZnakowLabel.Text =
                            "Dla wybranej metody: \n max. " + mml.ToString() + " znaków";
                        oknoWiadomosci.MaxLength = mml;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    infoToolStripStatusLabel.Text = "Błąd przy wczytywniu danych z pliku " + fileName;
                    programFile = false;
                }
                finally
                {
                    if (br != null)
                        br.Close();
                    if (fs != null)
                        fs.Close();
                }  
            }
        }
        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = oknoZapisywania.ShowDialog();
            if (dr == DialogResult.OK && programFile)
            {
                try
                {
                    infoToolStripStatusLabel.Text = "Proszę czekać. Trwa zapisywanie... ";
                    Header.dataSize = (uint)Math.Max(lDataList.Count, rDataList.Count) * 4;
                    fs = new FileStream(oknoZapisywania.FileName, FileMode.Create, FileAccess.Write);
                    bw = new BinaryWriter(fs);               
                    bw.Write(Header.riffID);
                    bw.Write(Header.size);
                    bw.Write(Header.wavID);
                    bw.Write(Header.fmtID);
                    bw.Write(Header.fmtSize);
                    bw.Write(Header.format);
                    bw.Write(Header.channels);
                    bw.Write(Header.sampleRate);
                    bw.Write(Header.bytePerSec);
                    bw.Write(Header.blockSize);
                    bw.Write(Header.bit);
                    bw.Write(Header.dataID);
                    bw.Write(Header.dataSize);
                    for (int i = 0; i < Header.dataSize / Header.blockSize; i++)
                    {
                        if (i < lDataList.Count)
                            bw.Write((short)lDataList[i]);
                        else
                            bw.Write(0);
                        if (i < rDataList.Count)
                            bw.Write((short)rDataList[i]);
                        else
                            bw.Write(0);
                    }
                    infoToolStripStatusLabel.Text = "Zapisano dane do pliku " + oknoZapisywania.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    infoToolStripStatusLabel.Text = "Nie zapisano danych do pliku " + oknoZapisywania.FileName;
                    fileName = "";
                }
                finally
                {
                    if (bw != null)
                        bw.Close();
                    if (fs != null)
                        fs.Close();
                }
            }
            else if (!programFile)
                infoToolStripStatusLabel.Text = "Brak danych do zapisania! ";
        }

        private void LSBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LSBToolStripMenuItem.Checked = true;
            FFTToolStripMenuItem.Checked = false;
        }

        private void FFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LSBToolStripMenuItem.Checked = false;
            FFTToolStripMenuItem.Checked = true;
        }

        private void ukryj_Click(object sender, EventArgs e)
        {
            // zabezpieczenie przez brakiem wyboru pliku
            if (!programFile)
                infoToolStripStatusLabel.Text = "Najpierw wczytaj plik!" + fileName;
            else
            {
                // konwersja wiadomości ze string do tablicy bitów
                messageS = oknoWiadomosci.Text;
                messageB = Metody.GetBits(messageS);
                // ukrywanie wiadomości za pomocą ulepszonego algorytmu LSB
                if (LSBToolStripMenuItem.Checked == true)
                {
                    lDataList = Metody.LSBh(lDataList, messageB, layer, Header.bit);
                    rDataList = Metody.LSBh(rDataList, messageB, layer, Header.bit);
                    lDataList = Metody.AddSign(lDataList, 0, (short)(messageS.Length), 4, Header.bit);
                    rDataList = Metody.AddSign(rDataList, 0, (short)(messageS.Length), 4, Header.bit);
                    infoToolStripStatusLabel.Text = "Ukryto wiadomość w pliku " + fileName;
                }
                // ukrywanie wiadomości z wykorzystaniem FFT
                else
                {
                    lDataList = Metody.FFTh(lDataList, messageB, layer, Header.bit);
                    rDataList = Metody.FFTh(rDataList, messageB, layer, Header.bit);
                    lDataList = Metody.AddSign(lDataList, 1, (short)(messageS.Length), 4, Header.bit);
                    rDataList = Metody.AddSign(rDataList, 1, (short)(messageS.Length), 4, Header.bit);
                    infoToolStripStatusLabel.Text = "Ukryto wiadomość w pliku " + fileName;
                }
            }  
        }

        private void odczytaj_Click(object sender, EventArgs e)
        {
            oknoWiadomosci.Text = "";
            // zabezpieczenie przez brakiem wyboru pliku
            if (fileName == "")
                infoToolStripStatusLabel.Text = "Najpierw wczytaj plik!";
            else
            {
                bool exist = (
                    Metody.MessageExsist(lDataList, 4, Header.bit)[0] == 64 ||
                    Metody.MessageExsist(rDataList, 4, Header.bit)[0] == 64) ? true : false;
                // brak informacji o wiadomości w danych
                if (!exist)
                    infoToolStripStatusLabel.Text = "W pliku " + fileName + " nie odnaleziono wiadomości!";
                // odczytywanie wiadomośći
                else
                {
                    if (Metody.MessageExsist(lDataList, 4, Header.bit)[1] == 0)
                        oknoWiadomosci.Text = Metody.LSBu(lDataList, Metody.MessageExsist(lDataList, 4, Header.bit)[2], layer, Header.bit);    
                    else
                        oknoWiadomosci.Text = Metody.FFTu(lDataList, Metody.MessageExsist(lDataList, 4, Header.bit)[2], layer, Header.bit);
                    infoToolStripStatusLabel.Text = "Odczytano wiadomość z pliku " + fileName;
                }                
            }         
        }
        // wyswietlanie możliwej ilości znaków do ukrycia (odświeżanie)
        private void tesToolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            if (lDataList.Count > 0)
            {
                if (LSBToolStripMenuItem.Checked)
                    mml = Math.Min(lDataList.Count, maxLen) / 8;
                else
                    mml = Math.Min(lDataList.Count / max, maxLen) / 8;
                iloscZnakowLabel.Text =
                    "Dla wybranej metody: \n max. " + mml.ToString() + " znaków";
                if(oknoWiadomosci.Text.Length > mml)
                    oknoWiadomosci.Text = oknoWiadomosci.Text.Substring(0, mml);
                oknoWiadomosci.MaxLength = mml;
            }
        }
        

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void ukryj_MouseDown(object sender, MouseEventArgs e)
        {
            infoToolStripStatusLabel.Text = "Proszę czekać. Trwa ukrywanie... ";
        }

        private void odczytaj_MouseDown(object sender, MouseEventArgs e)
        {
            infoToolStripStatusLabel.Text = "Proszę czekać. Trwa odczytywanie... ";
        }
        // krotki opis programu
        private void opisProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string opis = "Program służący do ukrywania i w plikach .wav wiadomości tekstowych" +
            " o maksymalnej długości 2048 znaków, oraz do odczytywania informacji już ukrytych" +
            " w innych plikach. Do wyboru 2 metody ukrywania: ulepszona metoda najmniej znaczącego" +
            " bitu (LSB) i metoda wykorzystująca szybką transformatę Fouriera (FFT). \nAutor: Konrad Kapczuk.";
            MessageBox.Show(opis, "Opis programu");
        }
        // krotki opis metod
        private void opisMetodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string opis = "W zakładce Metody mamy do wyboru: \nLSB - wiadomość jest ukrywana" +
            " na trzeciej (licząc od najmniej znaczącego bitu) warstwie bitów. Po osadzeniu w pliku" +
            " wybranego znaku, wszystkie bity znajdujące się poniżej w danym bajcie są \"poprawiane\"" +
            " w celu zmiejszenia niezgodności dźwięku z oryginałem. \n FFT - w tej metodzie wykorzystano" +
            " szybką transformację Fouriera. Informacja ukryta jest w różnicy dwóch składowych częstotliwości dźwięku.";
            MessageBox.Show(opis, "Opis metod");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string opis = "Ukrywanie wiadomości: \n" +
            " 1. Wczytaj plik w formacie WAV w menu Plik -> Wczytaj \n" +
            " 2. Wybierz metodę ukrywania informacji w menu Metoda \n" +
            " 3. Wpisz w  pole treść informacji o odpowiedniej długości\n" +
            " 4. Wciśnij przycisk \"Ukryj wiadomość\" i poczekaj\n" +
            " 5. Zapisz plik z ukrytą informacją w menu Plik -> Zapisz" +
            "\n\nOdczytywanie wiadomości: \n" +
            " 1. Wczytaj plik z ukrytą informacją (format WAV) w menu Plik -> Wczytaj \n" +
            " 2. Wciśnij przycisk \"Odczytaj wiadomość\" i poczekaj\n" +
            " 3. Ukryta informacja pojawi się w oknie po prawej stronie";
            MessageBox.Show(opis, "Instrukcja obsługi");
        }

        private void zamknijPlikToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // zerowanie ustawien
            filePath = "";
            fileName = "";
            programFile = false;
            mml = 0;
            lDataList = new List<short>();
            rDataList = new List<short>();
            lNewDataList = new List<short>(lDataList);
            rNewDataList = new List<short>(rDataList);
            infoToolStripStatusLabel.Text = "Zakończono pracę na pliku. Wczytaj nowy plik.";
            nazwaPlikuLabel.Text = "Nie wybrano pliku.";
            oknoWiadomosci.Text = "";
            iloscZnakowLabel.Text = "";
        }
    }
}
