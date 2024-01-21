
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Ucak
{
    public int UcakId { get; set; }
    public string Model { get; set; }
    public string Marka { get; set; }
    public string SeriNo { get; set; }
    public int Kapasitesi { get; set; }
}

class Lokasyon
{
    public int LokasyonId { get; set; }
    public string Sehir { get; set; }
    public string Ulke { get; set; }
    public string Havaalani { get; set; }
    public bool AktifPasif { get; set; }
}

class Ucus
{
    public int UcusId { get; set; }
    public int LokasyonId { get; set; }
    public DateTime Tarih { get; set; }
    public DateTime Saat { get; set; }
    public Ucak Ucak { get; set; }
    public bool AktifPasif { get; set; }
}

class Rezervasyon
{
    public int Id { get; set; }
    public Ucus Ucus { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public int Yas { get; set; }
    public int BiletNo { get; set; }
}

class Program
{
    static List<Ucak> ucaklar = new List<Ucak>();
    static List<Lokasyon> lokasyonlar = new List<Lokasyon>();
    static List<Ucus> ucuslar = new List<Ucus>();
    static List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();

    static void Main()
    {
        do {
            // JSON dosyalarından verileri okuma
            ReadDataFromJsonFiles();

            // Örnek verileri eklemek için fonksiyon
            PopulateSampleData();

            Console.WriteLine("Lokasyon girin: ");
            string hedefSehir = Console.ReadLine();

            Lokasyon hedefLokasyon = lokasyonlar.Find(l => l.Sehir.Equals(hedefSehir, StringComparison.OrdinalIgnoreCase) && l.AktifPasif);

            if (hedefLokasyon != null)
            {
                List<Ucus> availableFlights = ucuslar.FindAll(u => u.LokasyonId == hedefLokasyon.LokasyonId && u.AktifPasif);

                Console.WriteLine($"{availableFlights.Count} adet sefer bulundu");

                foreach (Ucus u in availableFlights)
                {
                    Console.WriteLine($"{u.UcusId} {u.Ucak.Marka} {u.Ucak.Model} {u.Ucak.SeriNo} {u.Tarih} {u.Saat} (toplam {u.Ucak.Kapasitesi} koltuk müsait)");
                }

                Console.WriteLine("Uçuş seçiniz: ");
                int secilenUcusId = Convert.ToInt32(Console.ReadLine());

                Ucus secilenUcus = availableFlights.Find(u => u.UcusId == secilenUcusId);
                string ReadNonEmptyString()
                {
                    string input = Console.ReadLine();

                    while (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Hata: Boş bırakılamaz. Lütfen bir değer girin.");
                        input = Console.ReadLine();
                    }

                    return input;
                }

                int ReadValidIntegerInput()
                {
                    int result = 0;
                    bool isValidInput = false;

                    while (!isValidInput)
                    {
                        string input = Console.ReadLine();

                        if (string.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Hata: Boş bırakılamaz. Lütfen bir değer girin.");
                        }
                        else if (!int.TryParse(input, out result))
                        {
                            Console.WriteLine("Hata: Geçerli bir sayı değil. Lütfen geçerli bir sayı girin.");
                        }
                        else
                        {
                            isValidInput = true;
                        }
                    }

                    return result;
                }
                if (secilenUcus != null)
                {
                    Console.WriteLine("Ad: ");
                    string ad = ReadNonEmptyString();

                    Console.WriteLine("Soyad: ");
                    string soyad = ReadNonEmptyString();

                    Console.WriteLine("Yaş: ");
                    int yas = ReadValidIntegerInput();

                    if (secilenUcus.Ucak.Kapasitesi > rezervasyonlar.Count(r => r.Ucus.UcusId == secilenUcus.UcusId))
                    {
                        int yeniBiletNo = rezervasyonlar.Count + 1;
                        Rezervasyon yeniRezervasyon = new Rezervasyon
                        {
                            Id = rezervasyonlar.Count + 1,
                            Ucus = secilenUcus,
                            Ad = ad,
                            Soyad = soyad,
                            Yas = yas,
                            BiletNo = yeniBiletNo
                        };

                        rezervasyonlar.Add(yeniRezervasyon);
                        // Uçağın kapasitesini azalt
                        secilenUcus.Ucak.Kapasitesi--;
                        string lokasyonBilgisi = $"{hedefLokasyon.Sehir} Havalimanı";
                        string ucmakBilgisi = $"{secilenUcus.Ucak.Marka} {secilenUcus.Ucak.Model} model uçağa";

                        Console.WriteLine($"{secilenUcus.Tarih} {secilenUcus.Saat} {lokasyonBilgisi}'ndan kalkacak olan {ucmakBilgisi} ait {secilenUcus.UcusId} numaralı uçuşa 1 adet rezervasyon yapılmıştır. Bilet no: {yeniBiletNo}");

                    }
                    else
                    {
                        Console.WriteLine("Üzgünüz, seçilen uçuş için boş koltuk kalmamıştır.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz uçuş seçildi.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz lokasyon.");
            }

            // JSON dosyalarına verileri kaydetme
            SaveDataToJsonFiles();
            Console.WriteLine("Başka bir işlem yapmak ister misiniz? (e/h)");

        } while (Console.ReadLine().Trim().Equals("e", StringComparison.OrdinalIgnoreCase));
    }
    static void ReadDataFromJsonFiles()
    {
        if (File.Exists("ucaklar.json"))
        {
            string ucaklarJson = File.ReadAllText("ucaklar.json");
            ucaklar = JsonConvert.DeserializeObject<List<Ucak>>(ucaklarJson);
        }

        if (File.Exists("lokasyonlar.json"))
        {
            string lokasyonlarJson = File.ReadAllText("lokasyonlar.json");
            lokasyonlar = JsonConvert.DeserializeObject<List<Lokasyon>>(lokasyonlarJson);
        }

        if (File.Exists("ucuslar.json"))
        {
            string ucuslarJson = File.ReadAllText("ucuslar.json");
            ucuslar = JsonConvert.DeserializeObject<List<Ucus>>(ucuslarJson);
        }

        if (File.Exists("rezervasyonlar.json"))
        {
            string rezervasyonlarJson = File.ReadAllText("rezervasyonlar.json");
            rezervasyonlar = JsonConvert.DeserializeObject<List<Rezervasyon>>(rezervasyonlarJson);
        }
    }

    static void SaveDataToJsonFiles()
    {
        string ucaklarJson = JsonConvert.SerializeObject(ucaklar, Formatting.Indented);
        File.WriteAllText("ucaklar.json", ucaklarJson);

        string lokasyonlarJson = JsonConvert.SerializeObject(lokasyonlar, Formatting.Indented);
        File.WriteAllText("lokasyonlar.json", lokasyonlarJson);

        string ucuslarJson = JsonConvert.SerializeObject(ucuslar, Formatting.Indented);
        File.WriteAllText("ucuslar.json", ucuslarJson);

        string rezervasyonlarJson = JsonConvert.SerializeObject(rezervasyonlar, Formatting.Indented);
        File.WriteAllText("rezervasyonlar.json", rezervasyonlarJson);
    }

    static void PopulateSampleData()
    {
        ReadDataFromJsonFiles();
        if (ucaklar.Count == 0)
        {
            ucaklar.Add(new Ucak { UcakId = 1, Model = "737", Marka = "Boeing", SeriNo = "12345", Kapasitesi = 100 });
            ucaklar.Add(new Ucak { UcakId = 2, Model = "A320", Marka = "Airbus", SeriNo = "67890", Kapasitesi = 150 });
            ucaklar.Add(new Ucak { UcakId = 3, Model = "747", Marka = "Boeing", SeriNo = "54321", Kapasitesi = 200 });
            ucaklar.Add(new Ucak { UcakId = 4, Model = "A380", Marka = "Airbus", SeriNo = "98765", Kapasitesi = 300 });
        }

        if (lokasyonlar.Count == 0)
        {
            lokasyonlar.Add(new Lokasyon { LokasyonId = 1, Sehir = "İstanbul", Ulke = "Türkiye", Havaalani = "IST", AktifPasif = true });
            lokasyonlar.Add(new Lokasyon { LokasyonId = 2, Sehir = "Ankara", Ulke = "Türkiye", Havaalani = "ESB", AktifPasif = true });
            lokasyonlar.Add(new Lokasyon { LokasyonId = 3, Sehir = "Paris", Ulke = "Fransa", Havaalani = "CDG", AktifPasif = true });
            lokasyonlar.Add(new Lokasyon { LokasyonId = 4, Sehir = "New York", Ulke = "ABD", Havaalani = "JFK", AktifPasif = true });
        }

        if (ucuslar.Count == 0)
        {
            DateTime now = DateTime.Now;

            ucuslar.Add(new Ucus { UcusId = 1, LokasyonId = 1, Tarih = now.AddDays(1), Saat = now.AddHours(15), Ucak = ucaklar[0], AktifPasif = true });
            ucuslar.Add(new Ucus { UcusId = 2, LokasyonId = 1, Tarih = now.AddDays(2), Saat = now.AddHours(17), Ucak = ucaklar[1], AktifPasif = true });
            ucuslar.Add(new Ucus { UcusId = 3, LokasyonId = 2, Tarih = now.AddDays(3), Saat = now.AddHours(14), Ucak = ucaklar[2], AktifPasif = true });
            ucuslar.Add(new Ucus { UcusId = 4, LokasyonId = 3, Tarih = now.AddDays(4), Saat = now.AddHours(18), Ucak = ucaklar[3], AktifPasif = true });
        }

        if (rezervasyonlar.Count == 0)
        {
            rezervasyonlar.Add(new Rezervasyon { Id = 1, Ucus = ucuslar[0], Ad = "Emrah", Soyad = "Sarıçiçek", Yas = 32, BiletNo = 1 });
            rezervasyonlar.Add(new Rezervasyon { Id = 2, Ucus = ucuslar[1], Ad = "Ayşe", Soyad = "Yılmaz", Yas = 28, BiletNo = 2 });
            rezervasyonlar.Add(new Rezervasyon { Id = 3, Ucus = ucuslar[2], Ad = "Mehmet", Soyad = "Aydın", Yas = 35, BiletNo = 3 });
            rezervasyonlar.Add(new Rezervasyon { Id = 4, Ucus = ucuslar[3], Ad = "Zeynep", Soyad = "Kaya", Yas = 25, BiletNo = 4 });
        }
        SaveDataToJsonFiles();

    }
}
