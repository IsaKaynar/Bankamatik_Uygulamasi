namespace Bankamatik
{
    internal class Program
    {
        double kasaBakiye = 250;
        string sifre = "1";
        static void Main(string[] args)
        {
            Program a = new Program();
            a.Calistir();
        }

        public void Calistir()
        {
            while (true)
            {
                bool kartSecim = KartliKartsizBölümSecimi(Giris());
                bool sifreDogruMu = KartliIslemBolumu(kartSecim);
                string menuSecim;
                do
                {
                    menuSecim = AnaMenü(sifreDogruMu);
                    MenuSecimi(menuSecim);
                } while (menuSecim != "0");
            }

        }

        static byte Giris()
        {
            byte secim;
            do
            {
                Console.WriteLine("\n******  BANKAMIZA HOŞ GELDİNİZ  ******\n");
                Console.WriteLine("\tKartlı işlem için\t1");
                Console.WriteLine("\tKartsız işlem için\t2");
                secim = Convert.ToByte(Console.ReadLine());
                Console.WriteLine();
            } while (secim != 1 && secim != 2);
            return secim;
        }

        static bool KartliKartsizBölümSecimi(int secim)
        {
            bool kartlibolum = false;
            //kartlibolum = secim == 1 ? kartlibolum = true : kartlibolum;
            if (secim == 1)
            {
                kartlibolum = true;
            }
            else if (secim == 2)
            {
                Console.WriteLine("Bu bölüm henüz aktif değildir.");
            }
            return kartlibolum;
        }

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
                    Console.WriteLine("Şifrenizi 3 kez yanlış girdiniz. Çıkış yapılıyor");
                }
            }
            return sifreDogruMu;
        }

        static string AnaMenü(bool sifreDogruMu)
        {
            string secim = "0";
            if (sifreDogruMu)
            {
                Console.WriteLine("*******  ANA MENÜ  *******");
                Console.WriteLine("Para Çekmek\t\t 1");
                Console.WriteLine("Para Yatırmak\t\t 2");
                Console.WriteLine("Para Transferleri\t 3");
                Console.WriteLine("Eğitim Ödemeleri\t 4");
                Console.WriteLine("Ödemeler\t\t 5");
                Console.WriteLine("Bilgi Güncelleme\t 6");
                Console.WriteLine("ÇIKIŞ\t\t\t 0");
                secim = Console.ReadLine();
                Console.WriteLine();
            }
            return secim;
        }

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

        public void ParaCekimTransferOdemeMethodu(string islem, string islem2)
        {
            double degiskenKasa = 0;
            Console.WriteLine("Kullanılabilir hesap bakiyeniz: " + kasaBakiye + " TL\n");
            
            Console.Write(islem + " tutarı giriniz: ");
            double tutar = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            string tekrarTutarGir = "2";
            while (tekrarTutarGir == "2")
            {
                Console.WriteLine(islem + " Tutar: " + tutar + " TL , dogru ise \tHerhangi bir tuşa basın.. \nyeniden tutar girmek için \t\t2 \nANA MENÜ için \t\t\t\t9 \nÇIKIŞ için \t\t\t\t0");
                tekrarTutarGir = Console.ReadLine();
                Console.WriteLine();
                if (tekrarTutarGir == "2")
                {
                    Console.Write(islem + " tutarı giriniz: ");
                    tutar = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine();
                }
                else if (tekrarTutarGir == "9")
                {
                    //Console.WriteLine("Ana menüye dönmeli");
                  
                }
                else if (tekrarTutarGir == "0")
                {
                    //Console.WriteLine("Çıkış Yapmalı");
                }
            }
            degiskenKasa = kasaBakiye - tutar;
            if (degiskenKasa >= 0)
            {
                kasaBakiye -= tutar;
                Console.WriteLine("İşleminiz Tamamlanmıştır!!!");
                Console.WriteLine("\nGüncel Bakiye: " + kasaBakiye + " TL\n");

            }
            double kullanilabilirTutar = 0;
            while (degiskenKasa < 0)
            {
                degiskenKasa = 0;
                Console.WriteLine("\nHesap bakiyesi yetersiz!\nEn fazla: " + kasaBakiye + " TL lik işlem yapabilirsiniz");
                Console.Write("Tekrar tutar girmek için (1), kullanılabilir bakiye tutarı için (2) ye basınız: ");
                kullanilabilirTutar = Convert.ToDouble(Console.ReadLine());
                if (kullanilabilirTutar == 1)
                {
                    Console.Write(islem + " tutarı giriniz: ");
                    tutar = Convert.ToDouble(Console.ReadLine());
                    degiskenKasa = kasaBakiye - tutar;
                    //Console.WriteLine("İşleminiz Tamamlanmıştır!!!");
                    //Console.WriteLine("\nGüncel Bakiye: " + kasaBakiye + " TL\n");
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
            //kasaBakiye -= tutar;
            //Console.WriteLine("İşleminiz Tamamlanmıştır!!!");
            //Console.WriteLine("\nGüncel Bakiye: " + kasaBakiye + " TL\n");

        }
            
        public void ParaCek()
        {
            ParaCekimTransferOdemeMethodu("Çekilecek", "çekilmiştir");
        }

        static string ParaYatirSecimi()
        {
            string secim;
            do
            {
                Console.WriteLine("Kredi Kartına\t\t 1");
                Console.WriteLine("Kendi Hesabınıza\t 2");
                secim = Console.ReadLine();
                Console.WriteLine();
            } while (secim != "1" && secim != "2");
            return secim;
        }

        public void KrediKartinaYatir(bool secim = true)
        {
            Console.WriteLine("16 haneli Kredi Kartı numaranızı giriniz");
            string kkNo = Console.ReadLine();
            bool sonuc = KrediKartNoDogruMu(kkNo);
            while (sonuc)
            {
                Console.WriteLine("Kart numarasını eksik girdiniz, tekrar giriniz");
                kkNo = Console.ReadLine();
                sonuc = KrediKartNoDogruMu(kkNo);
            }
            Console.WriteLine();
            ParaCekimTransferOdemeMethodu("Yatırılacak", "yatırılmıştır");

        }

        public bool KrediKartNoDogruMu(string kartNo)
        {
            int counter = 0;
            bool sonuc = true;
            char[] sayilar = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (kartNo.Length == 16)
            {
                for (int i = 0; i < kartNo.Length; i++)
                {
                    for (int j = 0; j < sayilar.Length; j++)
                    {
                        if (kartNo[i] == sayilar[j])
                        {
                            counter++;
                        }
                    }
                }
                if (counter == 16)
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }

        public void HesabaYatir(bool secim = false)
        {
            Console.WriteLine("Yatırılacak tutarı giriniz");
            double tutar = Convert.ToDouble(Console.ReadLine());
            kasaBakiye += tutar;
            Console.WriteLine("İşleminiz Tamamlanmıştır!!!\n");
            Console.Write("Güncel Bakiye: " + kasaBakiye + " TL\n");
            Console.WriteLine("\nAna menü için (9), ÇIKIŞ için (0) a basınız");
        }

        static string ParaTransferleriSecimi()
        {
            string secim;
            do
            {
                Console.WriteLine("Başka Hesaba EFT\t 1");
                Console.WriteLine("Başka Hesaba Havale\t 2");
                secim = Console.ReadLine();
                Console.WriteLine();
            } while (secim != "1" && secim != "2");
            return secim;
        }

        public void BaskaHesabaEFT()
        {
            Console.WriteLine("14 haneli EFT numarasını giriniz");
            string eftNo = Console.ReadLine();
            bool sonuc = HesapDogruMuEFT(eftNo);

            while (sonuc)
            {
                Console.WriteLine("EFT numarasını eksik girdiniz, tekrar giriniz");
                eftNo = Console.ReadLine();
                sonuc = HesapDogruMuEFT(eftNo);
            }

            ParaCekimTransferOdemeMethodu("Gönderilecek", "gönderilmiştir");

        }

        public void BaskaHesabaHavale()
        {
            Console.WriteLine("11 haneli Hesap numarasını giriniz");
            string hesapNo = Console.ReadLine();
            bool sonuc = HesapDogruMuHavale(hesapNo);
            while (sonuc)
            {
                Console.WriteLine("Hesap numarasını eksik girdiniz, tekrar giriniz");
                hesapNo = Console.ReadLine();
                sonuc = HesapDogruMuHavale(hesapNo);
            }

            ParaCekimTransferOdemeMethodu("Gönderilecek", "gönderilmiştir");

        }

        static bool HesapDogruMuHavale(string hesapNo)
        {
            int counter = 0;
            bool sonuc = true;
            char[] sayilar = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (hesapNo.Length == 11)
            {
                for (int i = 0; i < hesapNo.Length; i++)
                {
                    for (int j = 0; j < sayilar.Length; j++)
                    {
                        if (hesapNo[i] == sayilar[j])
                        {
                            counter++;
                        }
                    }
                }
                if (counter == 11)
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }

        static bool HesapDogruMuEFT(string eftNo)
        {
            int counter = 0;
            bool sonuc = true;
            char[] sayilar = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            while (eftNo.StartsWith("TR") ? false : true)
            {
                Console.WriteLine("EFT numarasının başında TR olmalı, tekrar giriniz");
                eftNo = Console.ReadLine();
            }
            if (eftNo.Length == 14)
            {
                for (int i = 2; i < eftNo.Length; i++)
                {
                    for (int j = 0; j < sayilar.Length; j++)
                    {
                        if (eftNo[i] == sayilar[j])
                        {
                            counter++;
                        }
                    }
                }
                if (counter == 12)
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }

        static void EgitimOdemeleri()
        {
            Console.WriteLine("Şu An Erişim Sağlanamıyor!!!\n");
        }

        static string Odemeler()
        {
            string secim = "0";
            Console.WriteLine("*******  Fatura Ödemeleri  *******");
            Console.WriteLine("Elektrik Faturası\t 1");
            Console.WriteLine("Su Faturası\t\t 2");
            Console.WriteLine("Telefon Faturası\t 3");
            Console.WriteLine("İnternet Faturası\t 4");
            Console.WriteLine("OGS Ödemeleri\t\t 5");
            secim = Console.ReadLine();
            Console.WriteLine();
            return secim;
        }

        public void ElektrikFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        public void SuFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        public void TelefonFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        public void InternetFaturasiOdemesi()
        {
            FaturaOdeme();
        }

        public void OgsOdemeleri()
        {
            FaturaOdeme();
        }

        public void FaturaOdeme()
        {
            ParaCekimTransferOdemeMethodu("Ödenecek", "ödenmiştir");
        }

        static string BilgiGuncelleme()
        {
            Console.WriteLine("Şifre değiştirmek için \t1");
            return Console.ReadLine();
        }

        public void SifreDegistirme()
        {
            Console.WriteLine("Lütfen yeni şifrenizi giriniz");
            string sifreYeni = Console.ReadLine();
            sifre = sifreYeni;
            Console.WriteLine("Şifreniz değiştirilmiştir.\n");
        }
        
    }
}