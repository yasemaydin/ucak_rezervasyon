# Uçak Rezervasyon Sistemi

Bu basit konsol uygulaması, uçak rezervasyon işlemlerini takip etmek için tasarlanmıştır. Uygulama, uçaklar, lokasyonlar, uçuşlar ve rezervasyonlar arasındaki ilişkileri yönetir.

**Kullanılan Teknolojiler**

- C#
- Newtonsoft.Json kütüphanesi

**Class Yapıları**

**Ucak**

- **UcakId** : Uçağın benzersiz kimliği.
- **Model** : Uçağın modeli (örn. "737", "A320").
- **Marka** : Uçağın markası.
- **SeriNo** : Uçağın seri numarası.
- **Kapasitesi** : Uçağın koltuk kapasitesi.

**Lokasyon**

- **LokasyonId** : Lokasyonun benzersiz kimliği.
- **Sehir** : Lokasyonun bulunduğu şehir.
- **Ulke** : Lokasyonun bulunduğu ülke.
- **Havaalani** : Lokasyonun havaalanı kodu.
- **AktifPasif** : Lokasyonun aktif veya pasif olma durumu.

**Ucus**

- **UcusId** : Uçuşun benzersiz kimliği.
- **LokasyonId** : Uçuşun başlangıç lokasyonu.
- **Tarih** : Uçuşun tarihi.
- **Saat** : Uçuşun saati.
- **Ucak** : Uçuşun yapıldığı uçağın referansı.
- **AktifPasif** : Uçuşun aktif veya pasif olma durumu.

**Rezervasyon**

- **Id** : Rezervasyonun benzersiz kimliği.
- **Ucus** : Rezervasyonun yapıldığı uçuşun referansı.
- **Ad** : Yolcunun adı.
- **Soyad** : Yolcunun soyadı.
- **Yas** : Yolcunun yaşı.
- **BiletNo** : Rezervasyonun bilet numarası.

**Metodlar**

**ReadDataFromJsonFiles()**

JSON dosyalarından verileri okuyan metod.

**SaveDataToJsonFiles()**

Verileri JSON dosyalarına kaydeden metod.

**PopulateSampleData()**

Örnek verileri eklemek için kullanılan metod.

**Nasıl Kullanılır?**

1. Konsolu başlatın.
2. İlgili verileri girebilmek için talimatları takip edin.
3. Uygulama, kullanıcının seçimlerine göre rezervasyon işlemlerini yönetir.
