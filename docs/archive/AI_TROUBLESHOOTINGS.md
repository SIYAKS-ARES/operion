# AI Servisi Sorun Giderme Rehberi

Bu dokuman, Operion uygulamasindaki AI (Gemini API) entegrasyonu ile ilgili sik karsilasilan sorunlari ve cozumlerini aciklar.

## Hizli Kontrol Listesi

1. **`.env` dosyasi proje kokunde var mi?**
   - Dosya: `c:\...\operion\.env`
   - Icerik: `GEMINI_API_KEY=YOUR_API_KEY_HERE`

2. **`.env` dosyasi output dizininde var mi?**
   - Dosya: `bin\Debug\net10.0-windows\.env`
   - Yoksa: `dotnet build` komutunu calistirin

3. **API Key gecerli mi?**
   - [Google AI Studio](https://aistudio.google.com/) adresinden kontrol edin

4. **Model dogru mu?**
   - App.config'de `AI_ENDPOINT` degerini kontrol edin
   - Calisan model: `gemini-flash-latest`

---

## Sik Karsilasilan Hatalar

### Hata: "API key not valid. Please pass a valid API key."

**Sebepler:**
1. `.env` dosyasi output dizininde yok
2. API key yanlis veya suresi dolmus
3. `.env` dosyasi formatinda hata var

**Cozum:**
```powershell
# 1. .env dosyasini kontrol edin
Get-Content "c:\...\operion\.env"

# 2. Output dizinini kontrol edin
Test-Path "c:\...\operion\bin\Debug\net10.0-windows\.env"

# 3. Yoksa build yapin
dotnet build
```

---

### Hata: "Rate limit exceeded" (429 Too Many Requests)

**Sebepler:**
1. Cok fazla istek gonderildi
2. Bazi modeller daha siki rate limit'e sahip

**Cozum:**
1. 1-2 dakika bekleyin
2. Farkli bir model deneyin (ornegin `gemini-flash-latest`)
3. Yeni bir API key olusturun

---

### Hata: "404 Not Found"

**Sebepler:**
1. Model adi yanlis veya mevcut degil
2. Endpoint URL'i yanlis

**Cozum:**
App.config'de `AI_ENDPOINT` degerini guncelleyin:
```xml
<add key="AI_ENDPOINT" value="https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent" />
```

---

## Konfigurasyon Dosyalari

### `.env` Dosyasi (Proje Kokunde)
```
GEMINI_API_KEY=YOUR_API_KEY_HERE
```

### App.config (AI Ayarlari)
```xml
<add key="AI_PROVIDER" value="Gemini" />
<add key="AI_ENDPOINT" value="https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent" />
<add key="AI_API_KEY" value="ENV:GEMINI_API_KEY" />
<add key="AI_MODEL" value="gemini-flash-latest" />
```

### operion.csproj (.env Kopyalama)
```xml
<None Include=".env" Condition="Exists('.env')">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

---

## API Key Nasil Alinir?

1. [Google AI Studio](https://aistudio.google.com/) adresine gidin
2. Google hesabinizla giris yapin
3. Sol menuden "Get API key" secenegine tiklayin
4. "Create API key" butonuna basin
5. Olusturulan keyi `.env` dosyasina kopyalayin

---

## Manuel Test

PowerShell ile API'yi test etmek icin:
```powershell
$body = @{ contents = @( @{ parts = @( @{ text = "Merhaba" } ) } ) } | ConvertTo-Json -Depth 5
$response = Invoke-RestMethod -Uri "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent?key=YOUR_API_KEY" -Method Post -Body $body -ContentType "application/json"
$response.candidates[0].content.parts[0].text
```

---

## Onemli Notlar

- `.env` dosyasi `.gitignore`'da olmali (API key'i commit etmeyin!)
- Uygulama yeniden baslatilmadan konfigurasyon degisiklikleri gecerli olmaz
- Rate limit hatasi aliyorsaniz, birkac dakika bekleyin veya yeni key olusturun
