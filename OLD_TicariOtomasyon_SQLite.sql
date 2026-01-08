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

-- Index'ler oluştur
CREATE INDEX IX_TBL_BANKALAR_FirmaID ON TBL_BANKALAR(FirmaID);
CREATE INDEX IX_TBL_FATURADETAY_FaturaID ON TBL_FATURADETAY(FaturaID);
CREATE INDEX IX_TBL_FIRMAHAREKETLER_UrunID ON TBL_FIRMAHAREKETLER(UrunID);
CREATE INDEX IX_TBL_FIRMAHAREKETLER_PersonelID ON TBL_FIRMAHAREKETLER(PersonelID);
CREATE INDEX IX_TBL_FIRMAHAREKETLER_FirmaID ON TBL_FIRMAHAREKETLER(FirmaID);
CREATE INDEX IX_TBL_MUSTERIHAREKETLER_UrunID ON TBL_MUSTERIHAREKETLER(UrunID);
CREATE INDEX IX_TBL_MUSTERIHAREKETLER_PersonelID ON TBL_MUSTERIHAREKETLER(PersonelID);
CREATE INDEX IX_TBL_MUSTERIHAREKETLER_MusteriID ON TBL_MUSTERIHAREKETLER(MusteriID);

-- Örnek veri ekle
INSERT INTO TBL_ADMIN (KullaniciAd, KullaniciSifre) VALUES ('admin', 'admin');

INSERT INTO TBL_ILLER (SEHIR) VALUES 
('İstanbul'),
('Ankara'),
('İzmir'),
('Bursa'),
('Antalya');

INSERT INTO TBL_ILCELER (ID, ILCE, SEHIR) VALUES 
(1, 'Kadıköy', 1),
(2, 'Beşiktaş', 1),
(3, 'Çankaya', 2),
(4, 'Konak', 3),
(5, 'Nilüfer', 4);

INSERT INTO TBL_PERSONELLER (PersonelAd, PersonelSoyad, PersonelTelefon, PersonelGorev) VALUES 
('Ahmet', 'Yılmaz', '05551234567', 'Satış Temsilcisi'),
('Ayşe', 'Demir', '05559876543', 'Muhasebeci');

INSERT INTO TBL_MUSTERILER (MusteriAd, MusteriSoyad, MusteriTelefon) VALUES 
('Mehmet', 'Kaya', '05551111111'),
('Fatma', 'Öz', '05552222222');

INSERT INTO TBL_URUNLER (UrunAd, UrunMarka, UrunModel, UrunAdet, UrunMaliyet, UrunSatisFiyat) VALUES 
('Laptop', 'Dell', 'Inspiron 15', 10, 8000, 12000),
('Mouse', 'Logitech', 'M100', 50, 50, 75),
('Klavye', 'Corsair', 'K70', 20, 300, 450);

INSERT INTO TBL_FIRMALAR (FirmaAd, FirmaTelefon1, FirmaMail) VALUES 
('ABC Teknoloji', '02161234567', 'info@abcteknoloji.com'),
('XYZ Yazılım', '02169876543', 'info@xyzyazilim.com');

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

