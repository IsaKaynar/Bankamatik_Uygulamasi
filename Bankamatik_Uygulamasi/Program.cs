using System.ComponentModel.Design;
using System.Reflection.PortableExecutable;

namespace Bankamatik
{
    internal class Program
    {
        double kasaBakiye = 250; // Başlangıç bakiyesi global değişken olarak tanımlanmıştır
        string sifre = "1"; // Aynı şekilde şifrede
        bool direkCikis = false; // işlemler esnasında çıkış yapabilmek için kullandığımız değişken
        static void Main(string[] args)
        {
            Program a = new Program();
            a.Calistir();
        }
        /// <summary>
        /// Sistemi döngü üzerinde çalıştıran ana method
        /// </summary>
        public void Calistir()
        {
            while (true)
            {
                bool kartSecim = KartliKartsizBolumSecimi(Giris());
                bool sifreDogruMu = KartliIslemBolumu(kartSecim);
                string menuSecim;
                do
                {
                    if (direkCikis == true)
                    {
                        direkCikis = false;
                        break;
                    }
                    menuSecim = AnaMenu(sifreDogruMu);
                    MenuSecimi(menuSecim);
                } while (menuSecim != "0");
            }

        }

        /// <summary>
        /// Kartlı kartsız seçimi için değer döndüren method
        /// </summary>
        /// <returns></returns>
        static string Giris()
        {
            string secim;
            do
            {
                Console.WriteLine("\n******  BANKAMIZA HOŞ GELDİNİZ  ******\n");
                Console.WriteLine("\tKartlı işlem için\t1");
                Console.WriteLine("\tKartsız işlem için\t2");
                secim = Console.ReadLine();
                Console.WriteLine();
            } while (secim != "1" && secim != "2");
            return secim;
        }

        /// <summary>
        /// Aldığı değere göre kartlı kartsız seçimi yapan method
        /// </summary>
        /// <param name="secim"></param>
        /// <returns></returns>
        static bool KartliKartsizBolumSecimi(string secim)
        {
            bool kartlibolum = false;
            //kartlibolum = secim == 1 ? kartlibolum = true : kartlibolum;
            if (secim == "1")
            {
                kartlibolum = true;
            }
            else if (secim == "2")
            {
                Console.WriteLine("Bu bölüm henüz aktif değildir.");
            }
            return kartlibolum;
        }

        /// <summary>
        /// Kartlı işlem bölümü girişi için şifre kontrolü yapan method
        /// </summary>
        /// <param name="secim"></param>
        /// <returns></returns>
        public bool KartliIslemBolumu(bool secim)
        {
            bool sifreDogruMu = false;
            string girilenSifre;
            if (secim)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Şifre giriniz");
                    girilenSifre = Console.ReadLine();
                    Console.WriteLine();
                    if (girilenSifre == sifre)
                    {
                        sifreDogruMu = true;
                        break;
                    }
                }
                if (sifreDogruMu == false)
                    Console.WriteLine("Şifrenizi 3 kez yanlış girdiniz. Çıkış yapılıyor");
            }
            return sifreDogruMu;
        }

        /// <summary>
        /// Menüdeki seçeneklere göre değer döndüren method
        /// </summary>
        /// <param name="sifreDogruMu"></param>
        /// <returns></returns>
        static string AnaMenu(bool sifreDogruMu)
        {
            string secim = "0";
            if (sifreDogruMu)
            {
                Console.WriteLine("\t*******  ANA MENÜ  *******");
                Console.WriteLine("\tPara Çekmek\t\t 1");
                Console.WriteLine("\tPara Yatırmak\t\t 2");
                Console.WriteLine("\tPara Transferleri\t 3");
                Console.WriteLine("\tEğitim Ödemeleri\t 4");
                Console.WriteLine("\tÖdemeler\t\t 5");
                Console.WriteLine("\tBilgi Güncelleme\t 6");
                Console.WriteLine("\tÇIKIŞ\t\t\t 0");
                secim = Console.ReadLine();
                Console.WriteLine();
            }
            return secim;
        }

        /// <summary>
        /// Menü işlemleri ve alt işlemlerin seçiminin yapıldığı switch methodu
        /// </summary>
        /// <param name="secim"></param>
        public void MenuSecimi(string secim)
        {
            switch (secim)
            {
                case "1":
                    ParaCek();
                    break;
                case "2":
                    string deger = ParaYatirSecimi();
                    switch (deger)
                    {
                        case "1":
                            KrediKartinaYatir();
                            break;
                        case "2":
                            HesabaYatir();
                            break;
                    }
                    break;
                case "3":
                    string deger2 = ParaTransferleriSecimi();
                    switch (deger2)
                    {
                        case "1":
                            BaskaHesabaEFT();
                            break;
                        case "2":
                            BaskaHesabaHavale();
                            break;
                    }
                    break;
                case "4":
                    EgitimOdemeleri();
                    break;
                case "5":
                    string deger3 = Odemeler();
                    switch (deger3)
                    {
                        case "1":
                            ElektrikFaturasiOdemesi();
                            break;
                        case "2":
                            SuFaturasiOdemesi();
                            break;
                        case "3":
                            TelefonFaturasiOdemesi();
                            break;
                        case "4":
                            InternetFaturasiOdemesi();
                            break;
                        case "5":
                            OgsOdemeleri();
                            break;
                    }
                    break;
                case "6":
                    string deger4 = BilgiGuncelleme();
                    switch (deger4)
                    {
                        case "1":
                            SifreDegistirme();
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Para çekim, havale, kk ödemesi, genel ödemelerin yapılmasını sağlayan method
        /// </summary>
        /// <param name="islem"></param>
        /// <param name="islem2"></param>
        public void ParaCekimTransferOdemeMethodu(string islem, string islem2)
        {
            double degiskenKasa = 0;
            Console.WriteLine("Kullanılabilir hesap bakiyeniz: " + kasaBakiye + " TL\n");
            Console.Write(islem + " tutarı giriniz: ");
            double tutar = Convert.ToDouble(Console.ReadLine());
            while (tutar <= 0)// Negatif sayı ve 0 girildiğinde aktif olur
            {
                Console.WriteLine("Negatif sayı veya 0 giremezsiniz, tekrar giriniz");
                tutar = Convert.ToDouble(Console.ReadLine());
            }
            Console.WriteLine();
            // Aşağıdaki while: doğrulama, tutar güncelleme, çıkış işlemini yapar
            string tekrarTutarGir = "2"; 
            while (tekrarTutarGir == "2")
            {
                Console.WriteLine(islem + " Tutar: " + tutar + " TL\n\nDogru ise \t\t\t1 \nYeniden tutar girmek için \t2 \nANA MENÜ \t\t\t9 \nÇIKIŞ \t\t\t\t0");
                tekrarTutarGir = Console.ReadLine();
                while (!(tekrarTutarGir == "1" || tekrarTutarGir == "2" || tekrarTutarGir == "9" || tekrarTutarGir == "0"))
                {
                    Console.WriteLine("\nYanlış bir tuşa bastınız!!\n\nDogru ise \t\t\t1 \nyeniden tutar girmek için \t2 \nANA MENÜ için \t\t\t9 \nÇIKIŞ için \t\t\t0");
                    tekrarTutarGir = Console.ReadLine();
                }
                Console.WriteLine();
                if (tekrarTutarGir == "2")
                {
                    Console.Write(islem + " tutarı giriniz: ");
                    tutar = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine();
                }
                else if (tekrarTutarGir == "0")
                {
                    direkCikis = true;
                }
            }
            if (!(tekrarTutarGir == "9" || tekrarTutarGir == "0")) // Yetersiz bakiye durumunda çıkış yapıldığında oluşan bug için eklenmiştir
            {
                degiskenKasa = kasaBakiye - tutar;
            }
            if (degiskenKasa >= 0 && tekrarTutarGir == "1")
            {
                kasaBakiye -= tutar;
                Console.WriteLine("İşleminiz Tamamlanmıştır!!!");
                Console.WriteLine("\nGüncel Bakiye: " + kasaBakiye + " TL\n");
            }
            //Aşağıdaki while: işlem bakiyesi yetersiz ise güncelleme veya mevcut bakiyeyi kullanmamızı sağlar
            double kullanilabilirTutar = 0;
            while (degiskenKasa < 0)
            {
                degiskenKasa = 0;
                Console.WriteLine("\nHesap bakiyesi yetersiz!\n\nEn fazla: " + kasaBakiye + " TL lik işlem yapabilirsiniz");
                Console.Write("Tekrar tutar girmek için \t\t1\nKullanılabilir bakiye tutarı için \t2\n");
                kullanilabilirTutar = Convert.ToDouble(Console.ReadLine());
                if (kullanilabilirTutar == 1)
                {
                    Console.Write(islem + " tutarı giriniz: ");
                    tutar = Convert.ToDouble(Console.ReadLine());
                    degiskenKasa = kasaBakiye - tutar;
                    if (degiskenKasa >= 0 && tekrarTutarGir == "1")
                    {
                        kasaBakiye -= tutar;
                        Console.WriteLine("\nİşleminiz Tamamlanmıştır!!!");
                        Console.WriteLine("\nGüncel Bakiye: " + kasaBakiye + " TL\n");
                    }
                }
                else if (kullanilabilirTutar == 2)
                {
                    tutar = kasaBakiye;
                    kasaBakiye -= tutar;
                    Console.WriteLine("\n" + tutar + " TL " + islem2);
                    Console.WriteLine("İşleminiz Tamamlanmıştır!!!");
                    Console.WriteLine("\nGüncel Bakiye: " + kasaBakiye + " TL\n");
                }
            }
        }
           
        /// <summary>
        /// Para çekimi için yönlendirme methodu
        /// </summary>
        public void ParaCek()
        {
            ParaCekimTransferOdemeMethodu("Çekilecek", "çekilmiştir");
        }

        /// <summary>
        /// Para yatırma seçeneklerine göre değer döndüren method
        /// </summary>
        /// <returns></returns>
        static string ParaYatirSecimi()
        {
            string secim;
            do
            {
                Console.WriteLine("\tKredi Kartına\t\t 1");
                Console.WriteLine("\tKendi Hesabınıza\t 2");
                secim = Console.ReadLine();
                Console.WriteLine();
            } while (secim != "1" && secim != "2");
            return secim;
        }

        /// <summary>
        /// Kredi Kartı ödeme methodu
        /// </summary>
        /// <param name="secim"></param>
        public void KrediKartinaYatir(bool secim = true)
        {
            Console.WriteLine("12 haneli Kredi Kartı numaranızı giriniz");
            string kkNo = Console.ReadLine();
            bool sonuc = HavaleVeyaKrediKartNoDogruMu(kkNo, 12, 0);
            KrediKVeHavaleCikisMethodu(kkNo, sonuc, 12, "Yatırılacak", "yatırılmıştır");
        }

        /// <summary>
        /// Kredi kartı veya havale işlemi sırasında çıkış yapmamızı sağlayan method 
        /// </summary>
        /// <param name="No"></param>
        /// <param name="sonuc"></param>
        /// <param name="kacHane"></param>
        /// <param name="islem1"></param>
        /// <param name="islem2"></param>
        public void KrediKVeHavaleCikisMethodu(string No, bool sonuc, byte kacHane, string islem1, string islem2)
        {
            while (sonuc)
            {
                Console.WriteLine("Kart numarasını eksik girdiniz, tekrar giriniz\tANAMENÜ  (9)\tÇIKIŞ  (0)");
                No = Console.ReadLine();
                if (No == "0")
                {
                    direkCikis = true;
                    break;
                }
                else if (No == "9")
                    break;
                sonuc = HavaleVeyaKrediKartNoDogruMu(No, kacHane, 0);
            }
            if (!(No == "9" || No == "0"))
            {
                Console.WriteLine();
                ParaCekimTransferOdemeMethodu(islem1, islem2);
            }
        }

        /// <summary>
        /// Havale veya kredi kartı numarasının doğruluğunu kontrol eden method
        /// </summary>
        /// <param name="kartNo"></param>
        /// <param name="kacBasamak"></param>
        /// <returns></returns>
        public bool HavaleVeyaKrediKartNoDogruMu(string kartNo, byte kacBasamak, byte fark)
        {
            int counter = 0;
            bool sonuc = true;
            char[] sayilar = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (kartNo.Length == kacBasamak)
            {
                for (int i = fark; i < kartNo.Length; i++)
                {
                    for (int j = 0; j < sayilar.Length; j++)
                    {
                        if (kartNo[i] == sayilar[j])
                        {
                            counter++;
                        }
                    }
                }
                if (counter == kacBasamak - fark)
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }

        /// <summary>
        /// Hesaba para yatırma methodu
        /// </summary>
        /// <param name="secim"></param>
        public void HesabaYatir(bool secim = false)
        {
            Console.WriteLine("Yatırılacak tutarı giriniz");
            double tutar = Convert.ToDouble(Console.ReadLine());
            kasaBakiye += tutar;
            Console.WriteLine("İşleminiz Tamamlanmıştır!!!\n");
            Console.Write("Güncel Bakiye: " + kasaBakiye + " TL\n\n");
        }

        /// <summary>
        /// Para transferi seçiminde değer döndüren method
        /// </summary>
        /// <returns></returns>
        static string ParaTransferleriSecimi()
        {
            string secim;
            do
            {
                Console.WriteLine("\tBaşka Hesaba EFT\t 1");
                Console.WriteLine("\tBaşka Hesaba Havale\t 2");
                secim = Console.ReadLine();
                Console.WriteLine();
            } while (secim != "1" && secim != "2");
            return secim;
        }

        /// <summary>
        /// EFT methodu
        /// </summary>
        public void BaskaHesabaEFT()
        {
            Console.WriteLine("14 haneli EFT numarasını giriniz");
            string eftNo = Console.ReadLine();
            bool sonuc = HesapDogruMuEFT(eftNo);
            while (sonuc)
            {
                Console.WriteLine("Kart numarasını eksik girdiniz, tekrar giriniz\tANAMENÜ  (9)\tÇIKIŞ  (0)");
                eftNo = Console.ReadLine();
                if (eftNo == "0")
                {
                    direkCikis = true;
                    break;
                }
                else if (eftNo == "9")
                    break;
                sonuc = HesapDogruMuEFT(eftNo);
            }
            if (!(eftNo == "9" || eftNo == "0"))
            {
                Console.WriteLine();
                ParaCekimTransferOdemeMethodu("Gönderilecek", "gönderilmiştir");
            }

        }
        
        /// <summary>
        /// Havale methodu
        /// </summary>
        public void BaskaHesabaHavale()
        {
            Console.WriteLine("11 haneli Hesap numarasını giriniz");
            string hesapNo = Console.ReadLine();
            bool sonuc = HavaleVeyaKrediKartNoDogruMu(hesapNo, 11, 0);
            KrediKVeHavaleCikisMethodu(hesapNo, sonuc, 11, "Gönderilecek", "gönderilmiştir");
        }

        /// <summary>
        /// EFT numarasının doğruluğunu kontrol eden method
        /// </summary>
        /// <param name="eftNo"></param>
        /// <returns></returns>
        public bool HesapDogruMuEFT(string eftNo)
        {
            while (eftNo.StartsWith("TR") ? false : true)
            {
                Console.WriteLine("EFT numarasının başında TR olmalı, tekrar giriniz");
                eftNo = Console.ReadLine();
            }
            bool sonuc = HavaleVeyaKrediKartNoDogruMu(eftNo, 14, 2);
            return sonuc;
        }

        static void EgitimOdemeleri()
        {
            Console.WriteLine("Şu An Erişim Sağlanamıyor!!!\n");
        }

        /// <summary>
        /// Ödemeler seçimine göre değer döndüren method
        /// </summary>
        /// <returns></returns>
        static string Odemeler()
        {
            string secim = "0";
            Console.WriteLine("\t*******  Ödemeler  *******");
            Console.WriteLine("\tElektrik Faturası\t 1");
            Console.WriteLine("\tSu Faturası\t\t 2");
            Console.WriteLine("\tTelefon Faturası\t 3");
            Console.WriteLine("\tİnternet Faturası\t 4");
            Console.WriteLine("\tOGS Ödemeleri\t\t 5");
            secim = Console.ReadLine();
            Console.WriteLine();
            return secim;
        }

        /// <summary>
        /// Fatura ödeme methodu
        /// </summary>
        public void ElektrikFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        /// <summary>
        /// Fatura ödeme methodu
        /// </summary>
        public void SuFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        /// <summary>
        /// Fatura ödeme methodu
        /// </summary>
        public void TelefonFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        /// <summary>
        /// Fatura ödeme methodu
        /// </summary>
        public void InternetFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        /// <summary>
        /// Fatura ödeme methodu
        /// </summary>
        public void OgsOdemeleri()
        {
            FaturaOdeme();
        }

        /// <summary>
        /// Ödeme methoduna yönlendiren method
        /// </summary>
        public void FaturaOdeme()
        {
            ParaCekimTransferOdemeMethodu("Ödenecek", "ödenmiştir");
        }

        /// <summary>
        /// Bilgi güncelleme seçimine göre değer döndüren method
        /// </summary>
        /// <returns></returns>
        static string BilgiGuncelleme()
        {
            Console.WriteLine("\tŞifre değiştirmek için \t1");
            return Console.ReadLine();
        }

        /// <summary>
        /// Şifre değiştirme methodu
        /// </summary>
        public void SifreDegistirme()
        {
            Console.WriteLine("Lütfen yeni şifrenizi giriniz");
            string sifreYeni = Console.ReadLine();
            sifre = sifreYeni;
            Console.WriteLine("Şifreniz değiştirilmiştir.\n");
        }
        
    }
}