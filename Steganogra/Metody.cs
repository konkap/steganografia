using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Math;
using System.Windows.Forms;

namespace Steganogra
{
    // metody pomocnicze
    public class Metody
    {
        // zamiana bitów (U2)
        public static byte[] toU2(byte[] bytes)
        {
            bool firstOne = false;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (firstOne)
                    bytes[bytes.Length - 1 - i] = bytes[bytes.Length - 1 - i] == 0 ? (byte)1 : (byte)0;
                if (bytes[bytes.Length - 1 - i] == 1 && !firstOne)
                {
                    firstOne = true;
                    bytes[bytes.Length - 1 - i] = 1;
                }
            }
            return bytes;
        }
        // konwersja próbki (short) do tablicy bitów (byte[])
        public static byte[] GetBitsFromSample(short input, ushort resolution)
        {          
            byte[] bytes = new byte[resolution];
            for (int i = resolution - 1; i >= 0; i--)
            {
                bytes[i] = (byte)(input & 1);
                input = (short)(input >> 1);
            }  
            return bytes;
        }
        // konwersja tablicy bitów (byte[]) do próbki (short)
        public static short GetSampleFromBits(byte[] input, ushort resolution)
        {
            short sample = 0;
            short znak = 1;
            if (input[0] == 1) 
            {
                znak = -1;
                input = Metody.toU2(input);
            }
            for (int i = 0; i < input.Length; i++)
                sample += (short)(input[input.Length - 1 - i] * Math.Pow(2, i));
            return (short)(sample * znak);
        }
        // konwersja wiadomości (string) do tablicy bitów (byte[])
        public static byte[] GetBits(string input)
        {
            byte[] letters = Encoding.ASCII.GetBytes(input);
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < letters.Length; i++)
                bytes.AddRange(Metody.GetBitsFromSample(letters[i], 8));
            return bytes.ToArray();
        }
        // konwersja tablicy bitów (byte[]) do wiadomości (string)
        public static string GetMessage(byte[] bytes, int mL)
        {
            int s;
            string message = "";
            for (int i = 0; i < mL; i++)
            {
                s = 0;
                for (int j = 7; j >= 0; j--)
                    s += (int)(bytes[i * 8 + j] * Math.Pow(2, 7 - j));
                message += (char)s;
            }
            return message;
        }
        // metoda ukrywająca wiadomości w danych (ulepszona LSB)
        public static List<short> LSBh(List<short> data, byte[] message, byte layer, ushort resolution)
        {
            if (data.Count() >= message.Count())
            {
                for (int i = 0; i < message.Count(); i++)
                {
                    if (Metody.GetBitsFromSample(data[i], resolution)[resolution - layer] == 0 && message[i] == 1)
                    {
                        byte[] sample = Metody.GetBitsFromSample(data[i], resolution);
                        sample[resolution - layer] = 1;
                        for (int j = resolution - layer + 1; j < resolution; j++)
                            sample[j] = 0; 
                        data[i] = GetSampleFromBits(sample, resolution);
                    }
                    else if (Metody.GetBitsFromSample(data[i], resolution)[resolution - layer] == 1 && message[i] == 0)
                    {
                        byte[] sample = Metody.GetBitsFromSample(data[i], resolution);
                        sample[resolution - layer] = 0;
                        for (int j = resolution - layer + 1; j < resolution; j++)
                            sample[j] = 1;
                        data[i] = GetSampleFromBits(sample, resolution);    
                    }
                }
            }    
            return data;
        }
        // metoda odczytująca wiadomość w danych (ulepszona LSB)
        public static string LSBu(List<short> data,int messageL, byte layer, ushort resolution)
        {
            if (data.Count() >= messageL * 8)
            {
                byte[] bits = new byte[8 * messageL];
                for (int i = 0; i < messageL * 8; i++)
                    bits[i] = Metody.GetBitsFromSample(data[i], resolution)[resolution - layer];
                return Metody.GetMessage(bits, messageL);
            }
            return "";
        }
        // metoda ukrywająca wiadomości w danych (wykorzystująca FFT)
        public static List<short> FFTh(List<short> data, byte[] message, byte layer, ushort resolution)
        {
            int max = 2048;                                 // max. rozmiar paczki do transformaty
            List<short> newData = new List<short>();
            Complex[] complex = new Complex[data.Count];    // tablice przed transformata (zespolone)
            Complex[] transform = new Complex[max];         // tablice pomocnicze to przechowywania wyniku transformaty paczki
            Complex[] afterTrns = new Complex[data.Count];  // tablice po transformacie (zespolone)
            Complex[] afterTrnsB = new Complex[data.Count]; // tablice po transformacie odwrotnej (zespolone)
            int ilosc = data.Count / max;                   // ilosc paczek
            int reszta = data.Count - (max * ilosc);        // reszta z danych podzielonych na paczki
            //********************* TRANSFORMACJA ***************************
            // zabezpieczenie przed przekroczeniem max. ilosci ukrytych znakow
            if (message.Length > ilosc - 1)
                MessageBox.Show("Wybraną metodą można ukryć w pliku max. " + ilosc / 8 + " znaków!");
            else
            {
                // zamiana liczb rzeczywistych na zespolone
                for (int i = 0; i < data.Count; i++)
                    complex[i] = new Complex(data.ElementAt(i), 0);
                // transformatowanie
                //for (int i = 0; i < ilosc; i++)
                for (int i = 0; i < message.Length; i++)
                {
                    transform = complex.Skip(i * max).Take(max).ToArray();
                    AForge.Math.FourierTransform.FFT(transform, FourierTransform.Direction.Forward);
                    for (int j = 0; j < max; j++)
                        afterTrns[j + i * max] = transform[j];   
                    // UKRYWANIE ZNAKU W PACZCE
                    if (i < message.Length)
                    {
                        // zabezpieczenie przed dwoma identycznymi składowymi
                        //afterTrns[i * max].Re += 5;
                        afterTrns[i * max].Re += 2; 
                        byte pierwszy =
                            (byte)(afterTrns[i * max].Re >= afterTrns[i * max + 2].Re ? 0 : 1);
                        if (message[i] != pierwszy)
                        {
                            Complex t = afterTrns[i * max];
                            afterTrns[i * max] = afterTrns[i * max + 2];
                            afterTrns[i * max + 2] = t;
                        }
                    }      
                }
                for (int k = ilosc * max; k < data.Count; k++)
                    afterTrns[k] = new Complex(data.ElementAt(k), 0);
                //************ transformacja odwrotna *********************
                //for (int i = 0; i < ilosc; i++)
                for (int i = 0; i < message.Length; i++)
                {
                    transform = afterTrns.Skip(i * max).Take(max).ToArray();
                    AForge.Math.FourierTransform.FFT(transform, FourierTransform.Direction.Backward);
                    for (int j = 0; j < max; j++)
                        afterTrnsB[j + i * max] = transform[j];
                }
                for (int k = ilosc * max; k < data.Count; k++)
                    afterTrnsB[k] = new Complex(data.ElementAt(k), 0);
                // zamiana po trans odwrotnej zespolonych na calkowite
                List<AForge.Math.Complex> lewa = afterTrnsB.ToList();
                newData = new List<short>();
                for (int i = 0; i < lewa.Count(); i++)
                {
                    newData.Add((short)lewa[i].Re);
                    if (newData[i] == 0)
                        newData[i] = data[i];
                }
            }
            return newData;
        }
        // metoda odczytująca wiadomość w danych (uwykorzystująca FFT)
        public static string FFTu(List<short> data, int messageL, byte layer, ushort resolution)
        {
            int max = 2048;                                 // max. rozmiar paczki do transformaty
            Complex[] complex = new Complex[data.Count];    // tablice przed transformacja (zespolone)
            Complex[] transform = new Complex[max];         // tablice pomocnicze to przechowywania wyniku transformacji paczki
            int ilosc = data.Count / max;                   // ilosc paczek
            int reszta = data.Count - (max * ilosc);        // reszta z danych podzielonych na paczki
            byte[] messageB = new byte[messageL * 8];       // wiadomość w postaci binarnej
            // zamiana liczb rzeczywistych na zespolone
            for (int i = 0; i < data.Count; i++)
                complex[i] = new Complex(data.ElementAt(i), 0);
            // transformatowanie
            for (int i = 0; i < messageL * 8; i++)
            {
                transform = complex.Skip(i * max).Take(max).ToArray();
                AForge.Math.FourierTransform.FFT(transform, FourierTransform.Direction.Forward);
                // odczytanie znaku z paczki;
                messageB[i] = (byte)(transform[0].Re > transform[2].Re ? 0 : 1);
            }
            return Metody.GetMessage(messageB,messageL); 
        }
        // umieszczanie w pliku informacji o ukrytej wiadomości
        public static List<short> AddSign(List<short> data, byte algo, short messageL, byte layer, ushort resolution)
        {
            // 8b = 64 istnieje, inne - nie istnieje
            // 1b = 0 LSB, 1 FFT
            // 14b = dłogość wiaomości (max. 2048 znaków)
            int i = data.Count - 100;                   // miejsce od którego jest zapisywane info o wiadomości
            byte[] mSign = { 0, 1, 0, 0, 0, 0, 0, 0 };  // 64 zapisane binarnie
            for (int j = 0; j < 8; j++)
            {
                if (Metody.GetBitsFromSample(data[j + i], resolution)[resolution - layer] != mSign[j])
                {
                    byte[] sample = Metody.GetBitsFromSample(data[j + i], resolution);
                    sample[resolution - layer] = (byte)(sample[resolution - layer] == 0 ? 1 : 0);
                    data[j + i] = GetSampleFromBits(sample, resolution);
                }
            }
            if (Metody.GetBitsFromSample(data[i + 8], resolution)[resolution - layer] != algo)
            {
                byte[] sample = Metody.GetBitsFromSample(data[i + 8], resolution);
                sample[resolution - layer] = (byte)(sample[resolution - layer] == 0 ? 1 : 0);
                data[i + 8] = GetSampleFromBits(sample, resolution);
            }
            byte[] ilosc = Metody.GetBitsFromSample(messageL, 14);
            i += 9;
            for (int j = 0; j < 14; j++)
            {
                if (Metody.GetBitsFromSample(data[j + i], resolution)[resolution - layer] != ilosc[j])
                {
                    byte[] sample = Metody.GetBitsFromSample(data[j + i], resolution);
                    sample[resolution - layer] = (byte)(sample[resolution - layer] == 0 ? 1 : 0);
                    data[j + i] = GetSampleFromBits(sample, resolution);
                }
            }
            return data; 
        }        
        // metoda poszukująca wiadomości w danych
        public static short[] MessageExsist(List<short> data, byte layer, ushort resolution)
        {
            short[] info = { 0, 0, 0 };
            int i = data.Count - 100;
            byte[] bits = new byte[8];
            for (int j = 0; j < 8; j++)
                bits[j] = Metody.GetBitsFromSample(data[i + j], resolution)[resolution - layer];
            info[0] = (short)Metody.GetSampleFromBits(bits,(ushort)bits.Length);
            if (info[0] != 64)
                return info;
            info[1] = Metody.GetBitsFromSample(data[i + 8], resolution)[resolution - layer];
            bits = new byte[14];
            i += 9;
            for (int j = 0; j < 14; j++)
                bits[j] = Metody.GetBitsFromSample(data[j + i], resolution)[resolution - layer];
            info[2] = Metody.GetSampleFromBits(bits, (ushort)bits.Length);
            return info;
        }
    }
}
