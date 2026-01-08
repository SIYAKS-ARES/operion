-- SQLite Uyumlu Ticari Otomasyon Veritabanı Script'i
-- SQL Server'dan SQLite'a dönüştürülmüştür
-- .NET 10 / operion projesi için güncellenmiştir

-- Tabloları oluştur
CREATE TABLE TBL_ADMIN (
    KullaniciAd TEXT,
    KullaniciSifre TEXT
);

CREATE TABLE TBL_ILLER (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    SEHIR TEXT
);

CREATE TABLE TBL_ILCELER (
    ID INTEGER PRIMARY KEY,
    ILCE TEXT,
    SEHIR INTEGER
);

CREATE TABLE TBL_FIRMALAR (
    FirmaID INTEGER PRIMARY KEY AUTOINCREMENT,
    FirmaAd TEXT,
    FirmaYetkiliGorev TEXT,
    FirmaYetkiliAdSoyad TEXT,
    FirmaYetkiliTC TEXT,
    FirmaSektor TEXT,
    FirmaTelefon1 TEXT,
    FirmaTelefon2 TEXT,
    FirmaTelefon3 TEXT,
    FirmaMail TEXT,
    FirmaFax TEXT,
    FirmaSehir TEXT,
    FirmaIlce TEXT,
    FirmaAdres TEXT,
    FirmaVergiDairesi TEXT,
    FirmaOzelKod TEXT,
    FirmaOzelKod2 TEXT,
    FirmaOzelKod3 TEXT,
    FirmaLogo BLOB
);

CREATE TABLE TBL_BANKALAR (
    BankaID INTEGER PRIMARY KEY AUTOINCREMENT,
    BankaAd TEXT,
    BankaSube TEXT,
    BankaIBAN TEXT,
    BankaHesapNo TEXT,
    BankaYetkili TEXT,
    BankaTarih TEXT,
    BankaHesapTuru TEXT,
    FirmaID INTEGER,
    BankaIl TEXT,
    BankaIlce TEXT,
    BankaTelefon TEXT,
    FOREIGN KEY (FirmaID) REFERENCES TBL_FIRMALAR(FirmaID)
);

CREATE TABLE TBL_PERSONELLER (
    PersonelID INTEGER PRIMARY KEY AUTOINCREMENT,
    PersonelAd TEXT,
    PersonelSoyad TEXT,
    PersonelTelefon TEXT,
    PersonelTC TEXT,
    PersonelMail TEXT,
    PersonelSehir TEXT,
    PersonelIlce TEXT,
    PersonelAdres TEXT,
    PersonelGorev TEXT,
    PersonelFoto BLOB
);

CREATE TABLE TBL_MUSTERILER (
    MusteriID INTEGER PRIMARY KEY AUTOINCREMENT,
    MusteriAd TEXT,
    MusteriSoyad TEXT,
    MusteriTelefon TEXT,
    MusteriTelefon2 TEXT,
    MusteriTC TEXT,
    MusteriMail TEXT,
    MusteriSehir TEXT,
    MusteriIlce TEXT,
    MusteriAdres TEXT,
    MusteriVergiDaire TEXT
);

CREATE TABLE TBL_URUNLER (
    UrunID INTEGER PRIMARY KEY AUTOINCREMENT,
    UrunAd TEXT,
    UrunMarka TEXT,
    UrunModel TEXT,
    UrunYil TEXT,
    UrunAdet INTEGER,
    UrunMaliyet REAL,
    UrunSatisFiyat REAL,
    UrunDetay TEXT,
    UrunResim BLOB
);

CREATE TABLE TBL_STOKLAR (
    StokID INTEGER PRIMARY KEY AUTOINCREMENT,
    StokTur TEXT,
    StokAdet TEXT
);

CREATE TABLE TBL_FATURABILGI (
    FaturaID INTEGER PRIMARY KEY AUTOINCREMENT,
    FaturaSeri TEXT,
    FaturaSiraNo TEXT,
    FaturaTarih TEXT,
    FaturaSaat TEXT,
    FaturaVergiDairesi TEXT,
    FaturaAlici TEXT,
    FaturaTeslimEden TEXT,
    FaturaTeslimAlan TEXT
);

CREATE TABLE TBL_FATURADETAY (
    FaturaUrunID INTEGER PRIMARY KEY AUTOINCREMENT,
    UrunAd TEXT,
    Miktar INTEGER,
    Fiyat REAL,
    Tutar REAL,
    FaturaID INTEGER,
    FOREIGN KEY (FaturaID) REFERENCES TBL_FATURABILGI(FaturaID)
);

CREATE TABLE TBL_FIRMAHAREKETLER (
    HareketID INTEGER PRIMARY KEY AUTOINCREMENT,
    UrunID INTEGER,
    Adet INTEGER,
    PersonelID INTEGER,
    FirmaID INTEGER,
    Fiyat REAL,
    Toplam REAL,
    FaturaID INTEGER,
    Tarih TEXT,
    Notlar TEXT,
    FOREIGN KEY (UrunID) REFERENCES TBL_URUNLER(UrunID),
    FOREIGN KEY (PersonelID) REFERENCES TBL_PERSONELLER(PersonelID),
    FOREIGN KEY (FirmaID) REFERENCES TBL_FIRMALAR(FirmaID)
);

CREATE TABLE TBL_MUSTERIHAREKETLER (
    HareketID INTEGER PRIMARY KEY AUTOINCREMENT,
    UrunID INTEGER,
    Adet INTEGER,
    PersonelID INTEGER,
    MusteriID INTEGER,
    Fiyat REAL,
    Toplam REAL,
    FaturaID INTEGER,
    Tarih TEXT,
    Notlar TEXT,
    FOREIGN KEY (UrunID) REFERENCES TBL_URUNLER(UrunID),
    FOREIGN KEY (PersonelID) REFERENCES TBL_PERSONELLER(PersonelID),
    FOREIGN KEY (MusteriID) REFERENCES TBL_MUSTERILER(MusteriID)
);

CREATE TABLE TBL_GIDERLER (
    GiderID INTEGER PRIMARY KEY AUTOINCREMENT,
    GiderAy TEXT,
    GiderYil TEXT,
    GiderElektrik REAL,
    GiderSu REAL,
    GiderDogalgaz REAL,
    GiderInternet REAL,
    GiderMaaslar REAL,
    GiderEkstra REAL,
    GiderNotlar TEXT
);

CREATE TABLE TBL_NOTLAR (
    NotID INTEGER PRIMARY KEY AUTOINCREMENT,
    NotTarih TEXT,
    NotSaat TEXT,
    NotBaslik TEXT,
    NotDetay TEXT,
    NotOlusturan TEXT,
    NotHitap TEXT
);

CREATE TABLE TBL_KODLAR (
    FIRMAKOD1 TEXT,
    FIRMAKOD2 TEXT,
    FIRMAKOD3 TEXT
);

-- ==========================================
-- TEMİZLENMİŞ VE DÜZENLENMİŞ GERÇEKÇİ VERİ SETİ (2025-2026)
-- ==========================================

-- 1. YÖNETİCİ
INSERT INTO TBL_ADMIN (KullaniciAd, KullaniciSifre) VALUES ('admin', 'admin');

-- 2. ŞEHİRLER (Örnek)
INSERT INTO TBL_ILLER (SEHIR) VALUES ('İstanbul'), ('Ankara'), ('İzmir'), ('Bursa'), ('Antalya');
INSERT INTO TBL_ILCELER (ID, ILCE, SEHIR) VALUES (1, 'Kadıköy', 1), (2, 'Çankaya', 2), (3, 'Konak', 3), (4, 'Nilüfer', 4);

-- 3. PERSONELLER (Ekip)
INSERT INTO TBL_PERSONELLER (PersonelAd, PersonelSoyad, PersonelTelefon, PersonelGorev) VALUES 
('Ahmet', 'Yılmaz', '0555 111 22 33', 'Satış Müdürü'),
('Ayşe', 'Demir', '0554 222 33 44', 'Muhasebe Sorumlusu'),
('Can', 'Öztürk', '0532 333 44 55', 'Teknik Servis'),
('Zeynep', 'Kaya', '0533 444 55 66', 'İnsan Kaynakları'),
('Mehmet', 'Çelik', '0505 555 66 77', 'Depo Sorumlusu');

-- 4. MÜŞTERİLER (Pazar)
INSERT INTO TBL_MUSTERILER (MusteriAd, MusteriSoyad, MusteriTelefon, MusteriMail, MusteriSehir, MusteriIlce, MusteriAdres) VALUES 
('Ali', 'Korkmaz', '0532 101 01 01', 'ali.korkmaz@mail.com', 'İstanbul', 'Kadıköy', 'Moda Cad. No:5'),
('Veli', 'Yıldız', '0533 102 02 02', 'veli.yildiz@mail.com', 'İstanbul', 'Beşiktaş', 'Çarşı İçi No:10'),
('Ayşe', 'Yılmaz', '0542 103 03 03', 'ayse.yilmaz@mail.com', 'Ankara', 'Çankaya', 'Tunali Hilmi Cad.'),
('Fatma', 'Demir', '0555 104 04 04', 'fatma.demir@mail.com', 'İzmir', 'Konak', 'Kordon Boyu'),
('Seda', 'Sayan', '0530 105 05 05', 'seda@sanat.com', 'İstanbul', 'Etiler', 'Nispetiye Cad.'),
('Acun', 'Ilıcalı', '0532 106 06 06', 'acun@medya.com', 'İstanbul', 'Sarıyer', 'Maslak 1453'),
('Cem', 'Yılmaz', '0533 107 07 07', 'cem@komedi.com', 'İstanbul', 'Levent', 'Kanyon'),
('Tarkan', 'Tevetoğlu', '0535 108 08 08', 'tarkan@music.com', 'İzmir', 'Urla', 'Doğa Evleri No:1');

-- 5. FİRMALAR (Tedarikçiler ve İş Ortakları)
INSERT INTO TBL_FIRMALAR (FirmaAd, FirmaTelefon1, FirmaMail, FirmaSektor, FirmaSehir, FirmaIlce, FirmaYetkiliAdSoyad, FirmaYetkiliGorev) VALUES 
('TechData A.Ş.', '0212 111 11 11', 'satis@techdata.com', 'Bilişim', 'İstanbul', 'Ümraniye', 'Hakan Yılmaz', 'Genel Müdür'),
('OfisDepo', '0216 222 22 22', 'info@ofisdepo.com', 'Kırtasiye', 'İstanbul', 'Ataşehir', 'Selin Demir', 'Satış Müdürü'),
('Aras Lojistik', '0212 333 33 33', 'kurumsal@aras.com', 'Lojistik', 'İstanbul', 'Esenyurt', 'Burak Can', 'Bölge Sorumlusu'),
('Garanti Mobilya', '0216 444 44 44', 'tasarim@garanti.com', 'Mobilya', 'Ankara', 'Siteler', 'Mehmet Usta', 'Üretim Müdürü'),
('YemekSepeti Corp', '0212 555 55 55', 'info@yemeksepeti.com', 'Gıda', 'İstanbul', 'Levent', 'Nevzat Aydın', 'CEO'),
('Turkcell Kurumsal', '0532 532 00 00', 'kurumsal@turkcell.com', 'Telekom', 'İstanbul', 'Maltepe', 'Kaan Terzioğlu', 'Direktör');

-- 6. ÜRÜNLER (Stok Envanteri)
INSERT INTO TBL_URUNLER (UrunAd, UrunMarka, UrunModel, UrunAdet, UrunMaliyet, UrunSatisFiyat, UrunDetay, UrunYil) VALUES 
('Laptop', 'Lenovo', 'ThinkPad X1', 12, 45000, 65000, 'Kurumsal iş dünyası için tasarlanmış yüksek performanslı ultrabook. Intel Core i7 işlemci, 16GB RAM ve 512GB SSD ile mühendislik ve yazılım geliştirme gibi yoğun işlemler için idealdir. Hafif karbon fiber kasa ve uzun pil ömrü sunar.', '2025'),
('Laptop', 'Apple', 'MacBook Pro', 8, 70000, 95000, 'Profesyonel yaratıcılar için M3 Pro çip, 14 inç Liquid Retina XDR ekran. Video kurgu, grafik tasarım ve 3D modelleme için mükemmel performans. 18 saat pil ömrü ve stüdyo kalitesinde mikrofonlar.', '2025'),
('Mouse', 'Logitech', 'MX Master 3', 45, 3000, 4500, 'Yazılımcılar ve tasarımcılar için ergonomik, çoklu bilgisayar kontrolü sağlayan kablosuz mouse. MagSpeed tekerlek ile saniyede 1000 satır kaydırma. USB-C şarjlı ve 70 gün pil ömrü.', '2024'),
('Klavye', 'Logitech', 'MX Keys', 30, 4000, 5500, 'Ofis ve yazılım geliştirme için sessiz, aydınlatmalı ve düşük profilli kablosuz klavye. Konforlu tuş yapısı ve çoklu cihaz desteği ile verimliliği artırır.', '2024'),
('Monitör', 'Dell', 'UltraSharp 27', 20, 15000, 22000, '4K çözünürlüklü, %99 sRGB renk doğruluğu sunan profesyonel IPS monitör. USB-C Hub özelliği ile tek kablo üzerinden görüntü ve şarj imkanı. Göz yorgunluğunu azaltan ComfortView teknolojisi.', '2025'),
('Yazıcı', 'Canon', 'ImageClass', 10, 8000, 12000, 'Küçük ofisler için hızlı ve kompakt renkli lazer yazıcı. Dakikada 21 sayfa baskı hızı, otomatik çift taraflı baskı ve Wi-Fi bağlantısı ile mobilden çıktı alma özelliği.', '2024'),
('Telefon', 'Samsung', 'S24 Ultra', 15, 60000, 80000, 'Yapay zeka özellikli, titanyum kasalı amiral gemisi telefon. S Pen desteği, 200MP kamera ve Snapdragon 8 Gen 3 işlemci ile en üst düzey mobil deneyim.', '2026'),
('Telefon', 'Apple', 'iPhone 15', 18, 50000, 65000, 'Günlük kullanım için ideal, dinamik ada ve 48MP kameralı akıllı telefon. A16 Bionic çip ile akıcı performans ve tüm gün süren pil ömrü. Dayanıklı seramik kalkan ön yüzey.', '2025');

-- 7. BANKALAR
INSERT INTO TBL_BANKALAR (BankaAd, BankaSube, BankaIBAN, BankaHesapNo, BankaYetkili, BankaTarih, BankaHesapTuru, FirmaID, BankaIl, BankaIlce, BankaTelefon) VALUES
('Akbank', 'Merkez', 'TR11 1111 1111', '1001001', 'Bireysel Portföy', '2025-01-01', 'Vadesiz TL', 1, 'İstanbul', 'Şişli', '0212 123 45 67'),
('Garanti', 'Ticari', 'TR22 2222 2222', '2002002', 'Ticari Müşteri Tem.', '2025-02-15', 'Şirket Hesabı', 2, 'İstanbul', 'Maslak', '0212 765 43 21'),
('İş Bankası', 'Kızılay', 'TR33 3333 3333', '3003003', 'Kobi Danışmanı', '2025-03-20', 'Kredi Hesabı', 3, 'Ankara', 'Kızılay', '0312 987 65 43');

-- 8. GİDERLER (Geçmiş 4 Ay)
INSERT INTO TBL_GIDERLER (GiderAy, GiderYil, GiderElektrik, GiderSu, GiderDogalgaz, GiderInternet, GiderMaaslar, GiderEkstra, GiderNotlar) VALUES
('Ekim', '2025', 2500, 500, 1500, 1200, 150000, 3000, 'Ofis Kira + Stopaj'),
('Kasım', '2025', 2800, 550, 2000, 1200, 150000, 2000, 'Mutfak Alışverişi'),
('Aralık', '2025', 3000, 600, 2500, 1200, 200000, 5000, 'Yılbaşı İkramiyeleri'),
('Ocak', '2026', 3200, 600, 3000, 1500, 180000, 1000, 'Yeni Yılda Zamlı Giderler');

-- 9. FATURALAR (Hem Alış Hem Satış İçin Alt Yapı)
-- Seriler: A (Satış), B (Alış)
INSERT INTO TBL_FATURABILGI (FaturaSeri, FaturaSiraNo, FaturaTarih, FaturaSaat, FaturaVergiDairesi, FaturaAlici, FaturaTeslimEden, FaturaTeslimAlan) VALUES
-- Satış Faturaları
('A', '000101', '2025-11-15', '14:30', 'Kadıköy', 'Ali Korkmaz', 'Ahmet Yılmaz', 'Ali Korkmaz'),
('A', '000102', '2025-11-20', '10:00', 'Beşiktaş', 'Veli Yıldız', 'Ayşe Demir', 'Veli Yıldız'),
('A', '000103', '2025-12-05', '16:00', 'Maslak', 'Acun Ilıcalı', 'Can Öztürk', 'Asistanı'),
('A', '000104', '2025-12-25', '09:00', 'Kadıköy', 'Seda Sayan', 'Ahmet Yılmaz', 'Seda Sayan'),
('A', '000105', '2026-01-05', '13:00', 'Levent', 'Cem Yılmaz', 'Ahmet Yılmaz', 'Cem Yılmaz'),
-- Alış Faturaları (Bizim firmalardan aldıklarımız)
('B', '000501', '2025-10-01', '09:00', 'Ümraniye', 'Operion A.Ş.', 'Hakan Yılmaz', 'Mehmet Çelik'),
('B', '000502', '2025-10-15', '11:00', 'Ataşehir', 'Operion A.Ş.', 'Selin Demir', 'Ahmet Yılmaz'),
('B', '000503', '2025-11-01', '14:00', 'Merter', 'Operion A.Ş.', 'Burak Can', 'Mehmet Çelik'),
('B', '000504', '2025-12-10', '10:00', 'Ümraniye', 'Operion A.Ş.', 'Hakan Yılmaz', 'Mehmet Çelik');

-- 10. FİRMA HAREKETLERİ (Stok Girişleri / Alışlarımız)
-- Biz TechData'dan Laptop alıyoruz, OfisDepo'dan Kırtasiye alıyoruz...
INSERT INTO TBL_FIRMAHAREKETLER (UrunID, Adet, PersonelID, FirmaID, Fiyat, Toplam, Tarih, Notlar, FaturaID) VALUES
-- Ekim: Büyük stok girişi
(1, 10, 5, 1, 45000, 450000, '2025-10-01', 'Sezon açılışı stok alımı', 6), -- FaturaID 6 (B-000501)
(2, 5, 5, 1, 70000, 350000, '2025-10-01', 'MacBook stoğu', 6),
(3, 50, 5, 2, 3000, 150000, '2025-10-15', 'Klavye-Mouse seti', 7),
(4, 50, 5, 2, 4000, 200000, '2025-10-15', 'Stok yenileme', 7),
-- Kasım: Lojistik ve ekleme
(7, 20, 5, 1, 60000, 1200000, '2025-11-01', 'Yeni S24 Ultra Lansman', 8),
-- Aralık: Yıl sonu hazırlık
(1, 5, 5, 1, 45000, 225000, '2025-12-10', 'Yıl sonu kampanya stoku', 9);

-- 11. MÜŞTERİ HAREKETLERİ (Stok Çıkışları / Satışlarımız)
INSERT INTO TBL_MUSTERIHAREKETLER (UrunID, Adet, PersonelID, MusteriID, Fiyat, Toplam, Tarih, Notlar, FaturaID) VALUES
-- Kasım Satışları
(1, 1, 1, 1, 65000, 65000, '2025-11-15', 'Bireysel Satış', 1), -- Fatura A-101
(3, 1, 1, 1, 4500, 4500, '2025-11-15', 'Yan ürün', 1),
(7, 1, 2, 2, 80000, 80000, '2025-11-20', 'Yüksek kâr marjlı satış', 2),
-- Aralık Satışları (Acun Ilıcalı Toplu Alım)
(2, 2, 3, 6, 95000, 190000, '2025-12-05', 'Prodüksiyon ekibi için', 3),
(5, 4, 3, 6, 22000, 88000, '2025-12-05', 'Ofis monitörler', 3),
(8, 5, 3, 6, 65000, 325000, '2025-12-05', 'Hediye telefonlar', 3),
-- Seda Sayan
(6, 1, 1, 5, 12000, 12000, '2025-12-25', 'Ofis yazıcısı', 4),
(4, 1, 1, 5, 5500, 5500, '2025-12-25', 'Pembe klavye isteği', 4),
-- Ocak 2026 (Cem Yılmaz)
(1, 1, 1, 7, 65000, 65000, '2026-01-05', 'Senaryo yazımı için', 5),
(3, 1, 1, 7, 4500, 4500, '2026-01-05', 'Yedek mouse', 5);

-- 12. NOTLAR (Ajanda)
INSERT INTO TBL_NOTLAR (NotTarih, NotSaat, NotBaslik, NotDetay, NotOlusturan, NotHitap) VALUES
('2026-01-10', '09:00', 'Yönetim Toplantısı', '2025 yılı ciro değerlendirmesi yapılacaktır.', 'Admin', 'Tüm Yöneticiler'),
('2026-01-15', '14:00', 'Vergi Ödemeleri', 'Geçici vergi son günü, ödemeler kontrol edilmeli.', 'Muhasebe', 'Ayşe Hanım'),
('2026-01-20', '10:30', 'TechData Ziyareti', 'Yeni sezon ürünler için tedarikçi ziyareti.', 'Satış', 'Ahmet Bey'),
('2026-02-01', '09:00', 'Sayım', 'Depo genel sayımı yapılacaktır.', 'Depo', 'Mehmet Çelik');

-- View'ları oluştur
CREATE VIEW BankaBilgileri AS
SELECT 
    b.BankaID, 
    b.BankaAd,
    b.BankaIl,
    b.BankaIlce,
    b.BankaYetkili,
    b.BankaTelefon,
    b.BankaTarih,
    b.BankaHesapNo,
    b.BankaHesapTuru,
    b.BankaSube,
    b.BankaIBAN,
    f.FirmaAd,
    f.FirmaID
FROM TBL_BANKALAR as b
INNER JOIN TBL_FIRMALAR as f 
ON b.FirmaID = f.FirmaID;

CREATE VIEW FirmaHareketler AS
SELECT 
    FH.HareketID,
    U.UrunAd,
    FH.Adet,
    P.PersonelAd,
    F.FirmaAd,
    FH.Fiyat,
    FH.Toplam,
    FB.FaturaAlici,
    FH.Tarih,
    FH.Notlar 
FROM TBL_FIRMAHAREKETLER AS FH
INNER JOIN TBL_URUNLER AS U ON FH.UrunID = U.UrunID
INNER JOIN TBL_PERSONELLER as P ON FH.PersonelID = P.PersonelID
INNER JOIN TBL_FIRMALAR AS F ON FH.FirmaID = F.FirmaID
INNER JOIN TBL_FATURABILGI AS FB ON FH.FaturaID = FB.FaturaID;

CREATE VIEW MusteriHareketler AS
SELECT 
    MH.HareketID,
    U.UrunAd,
    MH.Adet,
    P.PersonelAd,
    M.MusteriAd,
    MH.Fiyat,
    MH.Toplam,
    FB.FaturaAlici,
    MH.Tarih,
    MH.Notlar 
FROM TBL_MUSTERIHAREKETLER AS MH
INNER JOIN TBL_URUNLER AS U ON MH.UrunID = U.UrunID
INNER JOIN TBL_PERSONELLER AS P ON MH.PersonelID = P.PersonelID
INNER JOIN TBL_MUSTERILER AS M ON MH.MusteriID = M.MusteriID
INNER JOIN TBL_FATURABILGI AS FB ON MH.FaturaID = FB.FaturaID;

CREATE VIEW SonFirmaHareketler AS
SELECT 
    U.UrunAd,
    FH.Adet,
    F.FirmaAd,
    FH.Fiyat,
    FH.Toplam 
FROM TBL_FIRMAHAREKETLER AS FH
INNER JOIN TBL_URUNLER AS U ON FH.UrunID = U.UrunID
INNER JOIN TBL_FIRMALAR AS F ON FH.FirmaID = F.FirmaID
INNER JOIN TBL_FATURABILGI AS FB ON FH.FaturaID = FB.FaturaID;
