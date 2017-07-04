using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steganogra
{
    public struct WavHeader
    {
        public byte[] riffID;       // Identyfikator pliku RIFF
        public uint size;           // Długość danych w pliku w bajtach z pominięciem 8 bajtów nagłówka
        public byte[] wavID;        // Format pliku
        public byte[] fmtID;        // Początek części opisowej pliku
        public uint fmtSize;        // Rozmiar części opisowej
        public ushort format;       // Rodzaj kompresji
        public ushort channels;     // Liczba kanałów
        public uint sampleRate;     // Częstotliwość
        public uint bytePerSec;     // Częstotliwość bajtów
        public ushort blockSize;    // Rozmiar próbki
        public ushort bit;          // Rozdzielczość w bitach
        public byte[] dataID;       // Początek części z danymi
        public uint dataSize;       // Rozmiar bloku danych    
    }
}
