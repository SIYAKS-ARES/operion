# Proje Kod Standartları Kontrol Listesi

## Genel

1. Service ve Forms'larda hata değişkeni global tanımlanmamalı.
2. Class isimlerinde ki her kelimenin ilk harfi büyük olmalı (FrmHoiz)
3. Method isimlerinde ki her kelimenin ilk harfi büyük olmalı (GetMusteriBilgi)
4. Parametreler küçük harfle başlamalı, sonra gelen tüm kelimelerin ilk harfleri büyük olmalı. (subeKod, opAdi)
5. private değişkenler class'ların ilk başında tanımlanmalı.
6. property içinde kullanılan değişkenler '_' karakteriyle başlamalı.
7. Class isimleri ile dosya isimleri aynı olmalı. (SCommon, SCommon.cs)
8. Method'lar ve class'lar da kod açıklamaları olmalı. (/// Bu method.....)
9. Oluşturulan Rapor dosyalarının path'leri CommonFunction.GetReportDirectoryPath, kullanılan şablonların path'leri ise, CommonFunction.GetTemplateDirectoryPath kullanılarak alınmalıdır.

## Forms

1. Forms isimlendirmeleri Modul[.AltModül].Forms.kisa_ad. (Musteri.Kisi.Forms.kshvz)
2. Forms yardımcı class'lar F[kisa_ad] şeklinde verilmiş mi? (FKshvz)
3. Design'ların olduğu class'lar Frm[kisa_ad] şeklinde verilmiş mi? (FrmKshvz)
4. Interface'i çağıran tüm kodlarda if(hata!=null) kontrolü yapılmış mı?
5. Forms'lar daki DML işlemlerinde DMLManager kullanılmalı.
6. Ekran üzerinde ki kontrollerin ilk harfleri Büyük harfle başlamalı. ('Sorgula', 'Arama Yap')
7. DataGridView'e çift tıklandığında düzeltme yapılmalı.
8. Form'ların size'leri 770x700'den büyük olamaz.
9. Form'ların AutoScroll özelliği true olmalı.
10. Form'larda sql kullanılmamalı
11. Service, Business ve Util.DataAccess dll'leri referans edilmemeli.
12. Assembly ve file versiyonları verilmeli.
13. kul_ekran tablosuna kayıt edilirken versiyon belirtilmeli.
14. Sadece ortak ikonlar kullanılmalı
15. Form'un Text property'si kul_ekran.menudeki_adi ile aynı olmalı
16. kul_ekran.menudeki_adi Büyük karakter ile başlayıp küçük karakterlerle devam eden şeklinde olmalı
17. Ekranlarda uc ler dışında kontrol kullanılmamalı.

## Service

1. Service 'ler de try-catch'ler düzgün biçimde yazılmış mı?
2. sMan using ile kullanılmış mı?
3. string hata variable'ı null atanıp, daha sonra exception'da message'a eşitlenmiş mi?
4. Tüm methodlar string döndürmeli.
5. Servis isimlendirmeleri Modul.Service biçiminde yapılmış mı? (Common.Service)
6. Service'de ki class'ın ismi 'S ' harfi ile başlamalı. (SCommon)
7. SP çağrılmaları sMan.ExecuteSP şeklinde olmamalı, SPBuilder'dan SP dll'i oluşturulmalı.
8. Servislerde class bazlı değişken kesinlikle tanımlanmamalı, tüm tanımlamalar metodlar içinde olmalı

## Interface

1. Interface ismi Modul.Interface biçiminde mi?
2. Interface'te ki class ismi 'I' harfi ile başlamalı.

## Kontroller

### Standart Windows Kontrolleri

| Kontrol | Örnek İsimlendirme |
|---------|-------------------|
| Label | lblAd, lblSoyad, lblSubeAd, lblSubeKod |
| LinkLabel | llblAd, llblSoyad |
| Button | btnKaydet, btnDuzelt, btnSil, btnKapat |
| TextBox | txtAd, txtSoyad, txtSubeAd, txtSubeKod |
| MainMenu | mmenuDokuman, mmenuArsiv |
| CheckBox | chkSpor, chkKultur, chkGazete |
| RadioButton | rbtnEvli, rbtnBekar, rbtnYeniKayit |
| GroupBox | grpMedeniHal |
| PictureBox | pctImza, pctNufusCuzdan |
| Panel | pnlKimlik, pnlAdres, pnlTelefon |
| ListBox | lstKategoriTip, lstSubeKod |
| CheckedListBox | clstMailAdres |
| ComboBox | cmbSubeAd, cmbAdresKod |
| ListView | lviewGorusme |
| TreeView | tviewOrganizasyon |
| TabControl | tabMusteriTanim |
| DateTimePicker | dtpTarih, dtpIseGirisTarih |
| MonthCalendar | mcTakvim |
| HScrollBar | hsbGorus |
| VScrollBar | vsbGorus |
| Timer | timerKayit, timerLog |
| Splitter | splitterMusteri |
| TrackBar | trackbarFileUpload |
| ProgressBar | progbarFileUpload, progbarMuhasebe |
| RichTextBox | rtxtAciklama, rtxtOneri |
| ImageList | ilstMenu, ilstDokuman |
| HelpProvider | hprvSicilNo |
| ToolTip | ttipTara, ttipFarkliKaydet |
| ContextMenu | cmenuDosya |
| ToolBar | tbarKaydet |
| StatusBar | sbarKaydet, sbarrAc |
| NotifyIcon | niconUyari |
| ErrorProvider | eprvSubeKod |
| DataGridView | grdSube |

### Developer Express Kontrolleri

| Kontrol | Örnek İsimlendirme |
|---------|-------------------|
| BarManager | barmngMuhasebe |
| PopupMenu | popmenuMusteri |
| NavBarControl | navbarMuhasebe |
| VGridControl | vgrdParametre |
| GridControl | grdParametre |
| GridView | grdwParametre |
| SimpleButton | btnKaydet |
| DefaultLookAndFeel | dlfMusteri |
| XtraTabControl | xtabMusteri |
| ButtonEdit | ebtnKaydet |
| CalcEdit | calcKaydet |
| CheckEdit | chkFutbol, chkBasketbol |
| CheckedListBoxControl | clstMailAdres |
| ComboBoxEdit | cmbSubeKod |
| ControlNavigator | ctrlnavMusteri |
| DateEdit | dateDogumTarih |
| ImageEdit | imgMusteri |
| ImageListboxControl | imglistSubeAd |
| ListboxControl | lstSubeAd |
| LookUpEdit | lueSubeKod |
| MemoEdit | memoAciklama |
| PictureEdit | pctMusteri |
| ProgressBarControl | pbcUpload |
| RadioGroup | rgSpor |
| SpinEdit | spinAdres |
| TextEdit | txtAd |
| TimeEdit | timeBaslangicSaat |
| GrupControl | groupMedeniHal |
| HScrollBar | hsbMusteri |
| VScrollBar | vsbMusteri |
| ImageCollection | imgcollectCek |
| PanelKontrol | pnlMusteri |
| ToolTipController | ttcKimlik |

## Namespace

```
Firat.Musteri.Forms
Firat.Musteri.Service
Firat.Musteri.Interface
Firat.Musteri.Business
Firat.Musteri.SP
Firat.Musteri.Helper
Firat.Hesap.Forms
Firat.Hesap.Service
Firat.Hesap.Genel.Service
Firat.Hesap.Detay.Service
Firat.Hesap.Interface
Firat.Hesap.Genel.Interface
Firat.Hesap.Detay.Interface
Firat.Hesap.Business
Firat.Hesap.Genel.Business
Firat.Hesap.Detay.Business
Firat.Hesap.Sp
Firat.Hesap.Genel.Sp
Firat.Hesap.Detay.Sp
```

## Class

- FMusteri
- SMusteri
- IMusteri
- BMusteri
- SpMusteri (static T_MUSTERI, P_MUSTERI database'deki package name'leri)
- HMusteri

## Method

- MusteriEkle
- MusteriBul
- SP'ler için get_bakiye (database de ne ise aynı olacak)

## Public Değişkenler

```csharp
public string kisaAd
public long sicilNo
```

## Private Değişkenler

```csharp
private decimal _bakiye  // Sadece property'leri için tanımlanlar private değişkenler _ ile başlayacak
private int _adresKod
private string _kisaAd
```

## Property

- Bakiye (esas teşkil eden private değişken _bakiye olarak tanımlanmalı)
- AdresKod

## Exception

- ex, ex1, ex2

## Oracle DotNet Provider

```csharp
OracleConnection conn
OracleCommand cmd
OracleTransaction trans
OracleParameter prm
OracleDataAdapter da
OracleDataReader dr
OracleCommandBuilder cb
```

## System.Data

| Tip | Örnek İsimlendirme |
|-----|-------------------|
| DataSet | ds, dsEkran, dsOperator |
| DataTable | dt, dtEkran, dtOperator |
| DataView | dv, dvEkran, dvOperator |
| DataRow | drow, drowKisi, drowOperator |
| DataColumn | dcol, dcolSubeKod, dcolSubeAd |

## Control Library

- CtrlLibSubeKod
- CtrlLibHesapNo
- CtrlLibEkNo
- ucSubeKod
- ucHukukiYapi
- ucHesapNo

## Control Library Standartları

- User Kontrollere yazdığımız her property, metot vs.. x ile başlamalı. Nedeni ise kod geliştirirken kendi yazdığımız bu tür özelliklere intellesense'tan kolayca ulaşmaktır.
- Veritabanı ile ilişkisi olan her kontrolün xEkranParam Property'si tanımlamalı.
- Eğer bir veya birden fazla property'i set eden bir metot yazacaksak metodun adı "xSetParams" olmalı.
- Eğer Bir Value property lazımsa ismi xValue şeklinde olmalı ve bu değer set edildiğinde Text property'si de değiştirilmeli.
- Hesap No, Kisino, Vergi No gibi sayısal değerlerde long tipini kullanmalıyız.

## For Döngülerinde Kullanılan Counter'lar

- i, j, k

## Class'lardan Nesne Oluştururken

| Tip | Örnek İsimlendirme |
|-----|-------------------|
| OpenFileDialog | ofdUpload, ofdDownload |
| PrintDialog | pdDekont, pdFis |

## Programlama ve Kodlama ile İlgili Standartlar

### Açıklama Satırları (Comment)

Birden fazla açıklama satırı olacak ise:

```csharp
/*
 * Açıklama satırı (Created by …………….., DD/MM/YYYY)
 * Açıklama satırı (Fonksiyonu, uyarılar)
 * Açıklama satırı (Edited by …………….., DD/MM/YYYY, Neden edit edildiği)
 */
```

Tüm assembly, bu assembly'ler içerisindeki class ve bu class'lar içerisindeki metod'lar için yukarıda standart kullanılmalı.

Tek satırlık açıklamalar için `// …………` veya `/* ………….. */` kullanılabilir.

### Girintili Yazma (Indentation)

.Net editörü girintilerde default olarak 4 karakter boşluk bırakıyor. Bu standarta uyulmalı.

#### For Döngüsü

```csharp
for(int i=0; i<5; i++)
{
    // ...
}
```

#### If Koşulu

```csharp
if (a < b)
{
    // ...
}
else
{
    // ...
}
```

#### Exception

```csharp
try
{
    // ...
}
catch(Exception ex)
{
    // ...
}
```

### Assembly İçerisindeki SQL'ler Yazılımı

```csharp
string sql = @"SELECT t.kategori,
                      v.versiyon,
                      v.tarih,
                      v.op_adi,
                      v.dokuman_no
               FROM das_dokuman d,
                    das_dokuman_versiyon v,
                    das_kategori_tip t
               WHERE d.dokuman_no = v.dokuman_no
                 AND d.aktif_versiyon = v.versiyon
                 AND d.ana_kategori_tip = t.ana_tip
                 AND d.kategori_tip = t.tip
                 AND d.ana_kategori_tip = " + lueAnaKategori.EditValue.ToString() + @"
                 AND d.takip_no = '" + txtTakipNo.Text + @"'
               ORDER BY t.kategori, v.op_adi";
```

```csharp
string sql = @"UPDATE m_operator
               SET sube_kod = " + ucSubeKod.Text + @"
               WHERE op_adi = '" + FSubeDegistir.ekranPrm.kulFrm.OpAd + "'";
```

### String Concatenation İşlemleri

String concatenation işlemlerinde string class'ı yerine StringBuilder kullanılmalı.

**Yanlış Kullanım:**

```csharp
string sonuc;
for (int i = 0; i < 10; i++)
{
    sonuc += i.ToString();
}
```

**Doğru Kullanım:**

```csharp
StringBuilder sonuc = new StringBuilder();
for (int i = 0; i < 10; i++)
{
    sonuc.Append(i.ToString());
}
```

## Form Görsel Tasarım Standartları

- **Font:** Tahoma
- **Font-Size:** 8.25
- **Info (readonly veya disabled) alanlar için:** Web.LightYellow
- Labellar sağa veya sola yanaşık olabilir.

## SPObject Katmanı Standartları

Bu katmanda orta katmanda kullanılacak SP'leri çağıran sınıflar yazılacak. .Net-Oracle tür uyuşmazlığı nedeniyle Oracle tarafındaki rowtype veya özel type return eden SP'ler yazılırken body'si ile birlikte yazılacak. Yani bu SP'lerin SELECT kısmı .Net tarafında yazılacak. İsimlerin birebir aynı olmasına dikkat edilecek.

Bu katmanda OracleConnection kurulmayacak. Conn bilgisi procedure'lere parametre olarak çağrıldığı yerden (Services veya Business Object) gönderilecektir.

SPObjeleri yazılırken Her bir modül ayrı proje halinde olacak. Modüle ait Package'lerin her biri yeni bir .cs dosyası yani class olacak.

## BusinessObject Katmanı Standartları

Bu katmanda, birden fazla servis(modül) tarafından kullanılacak birden fazla SP objesini kullanan veya DML işlemi yapan anlamlı işlemler bütünü (havale, kredi kartı ödemesi vs, T_MUHASEBE gb)

## Service Katmanı Standartları

Client tarafından ilgili modülle alakalı istekleri karşılayacak olan katmandır. Yazılacak metodların başka servislerden de kullanılma ihtimali var ise bu metodların içeriği business object katmanında yazılıp bu katmandan sadece çağrılacaklardır. Yok sadece bu servise özel ise doğrudan bu katmanın içinde geliştirileceklerdir.

## Dataset, Datatable Standartları

Dataset ve datatable nesneleri tek satırlık kayıt içerseler bile size'ları çok yüksek oluyor. Mümkün mertebe bu nesneleri client tarafına parametrelerle geçmekte fayda var. Orta katman ile Oracle arasında aynı networkte oldukları için herhangi bir sorun yok. Client gönderilecek datatable larda mümkünse kayıt sayısı kontrolü koymak gerekmektedir. (rownum<100)

## Metod ve Parametreleri ile İlgili Açıklamalar

```csharp
/// <summary>
/// Data table'ı update eder
/// </summary>
/// <param name="ci">ClientInfo</param>
/// <param name="dt">DataTable</param>
/// <param name="onErrorRollBack">Hata durumunda rollback</param>
/// <param name="dtLast">DataTable'ın son hali</param>
/// <param name="rowsAffected">Etkilenen kayıt sayısı</param>
/// <returns>String döner, hata yoksa null döner</returns>
```
