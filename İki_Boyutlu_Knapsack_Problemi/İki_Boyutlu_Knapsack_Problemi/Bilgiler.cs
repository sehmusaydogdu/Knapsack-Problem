using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İki_Boyutlu_Knapsack_Problemi
{
    class Kisit1
    {
        public static double sabit = 150;  // Kısıt 1 in sağ taraf sabiti  (....<=12)
        public static double[] kisitKatSayilari = { 5,20,25,35,40,45,55,60 }; //1.Kısıtın katsayıları
    }
    class Kisit2
    {
        public static double sabit = 300; // Kısıt 2 nin sağ taraf sabiti  (....<=11)
        public static double[] kisitKatSayilari = {90,120,70,110,90,65,80,150}; //2.Kısıtın katsayıları
    }
    class AmacFonksiyonu
    {
       public static double[] max_z = { 10,20,30,40,50,60,70,80}; //Amaç fonksiyonunun katsayıları
    }
    class YeniKisit
    {
        public static double sabit;
        public static double[] yeniKisitKatsayilari = new double[AmacFonksiyonu.max_z.Count()];
    }

    class Bilgiler
    {
        double t1 = 0, t2 = 0;

        double[] gecici = new double[AmacFonksiyonu.max_z.Count()]; // (cj / aj) Oranlarını tutacak.

        List<int> sonuclar = new List<int>();  //Çözüm Kümesini Tutacak

        double cantaKapasitesi = 0;//Çanta başlangıçta sıfır olsun

        private void yeniKisitiOlustur()
        {
            for (int i = 0; i < AmacFonksiyonu.max_z.Count(); i++)
            {
                t1 += Kisit1.kisitKatSayilari[i];
                t2 += Kisit2.kisitKatSayilari[i];
            }
            t1 = (t1 - Kisit1.sabit) / Kisit1.sabit;
            t2 = (t2 - Kisit2.sabit) / Kisit2.sabit;

            for (int i = 0; i < AmacFonksiyonu.max_z.Count(); i++)
                YeniKisit.yeniKisitKatsayilari[i] = Kisit1.kisitKatSayilari[i] * t1 + Kisit2.kisitKatSayilari[i] * t2;
        }
        private void yeniSabitiOlustur()
        {
            YeniKisit.sabit = Kisit1.sabit * t1 + Kisit2.sabit * t2;
        }
        private int maxbul()
        {
            double enbuyuk =0;
            int yeri=-1;
            bool durum = false;
            for (int i = 0; i < AmacFonksiyonu.max_z.Count(); i++)
            {
                if (enbuyuk < gecici[i])
                {
                    enbuyuk =gecici[i];
                    yeri = i;
                    durum = true;
                }
            }
            if (durum)
            {
                gecici[yeri] = -1;
                return yeri;
            }
            else
                return -1;
        
        }
        private void sonucuHesapla()
        {
            //Şimdi elimde 1 kısıt ve bir amaç fonksiyonu var. 
            //Bunu şimdi tek boyutlu (boolean -(0-1)) tipli problem gibi çözeceğim.

            for (int i = 0; i < AmacFonksiyonu.max_z.Count(); i++)
                gecici[i] = AmacFonksiyonu.max_z[i] / YeniKisit.yeniKisitKatsayilari[i];

            int yeri;
            while ((yeri = maxbul()) >= 0 && (cantaKapasitesi + YeniKisit.yeniKisitKatsayilari[yeri]) < YeniKisit.sabit)
            {
                cantaKapasitesi += YeniKisit.yeniKisitKatsayilari[yeri];
                sonuclar.Add(yeri);
            }
         
        }
            
        private void sonuclariGoster()
        {
            double z = 0;
            Console.WriteLine("Çantaya giren elemanların indexleri şunlardır");
            foreach (var item in sonuclar)
                Console.WriteLine($"{item}   ");

            for (int i = 0; i < AmacFonksiyonu.max_z.Count(); i++)
            {
                bool aranan = sonuclar.Contains(i);
                if (aranan)
                {
                    z += AmacFonksiyonu.max_z[i];
                }
            }
            Console.WriteLine($"Amaç fonksiyonun değeri {z}");

        }
        public void Hesapla()
        {
            yeniKisitiOlustur(); //2 kısıt tek kısıta dönüştürüldü.
            yeniSabitiOlustur(); //Tek kısıt için yeni bir sabit oluşturuldu
            sonucuHesapla();
            sonuclariGoster();
        }
    }
}
