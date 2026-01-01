---
name: AI Implementation Plan - Gemini API
overview: Complete AI integration plan for operion using Google Gemini API. Updates AiService to support Gemini API format, configures App.config for Gemini, and implements UI integration for FrmRaporlar and FrmMail forms.
todos:
  - id: gemini-aiservice
    content: "Update AiService.cs to support Gemini API: BuildRequestBody method for Gemini request format, CallAiApiAsync for query parameter authentication, ParseResponse for Gemini response format"
    status: completed
  - id: gemini-appconfig
    content: Update App.config with Gemini API endpoint (gemini-1.5-flash model), provider set to Gemini, and ENV:GEMINI_API_KEY configuration
    status: completed
  - id: gemini-env-setup
    content: Set GEMINI_API_KEY environment variable and verify AiService.IsConfigured() returns true
    status: completed
    dependencies:
      - gemini-appconfig
  - id: frmraporlar-ui
    content: Add AI Özeti tab to FrmRaporlar.Designer.cs with all required controls (buttons, textboxes, progress bar, labels)
    status: completed
  - id: frmraporlar-data
    content: Implement PrepareReportDataForAi() method in FrmRaporlar.cs to extract and format report data from database
    status: completed
  - id: frmraporlar-ai-logic
    content: Implement btnOzetUret_Click async method with Gemini AI service calls, rate limiting, error handling, and result display
    status: completed
    dependencies:
      - gemini-aiservice
      - frmraporlar-ui
      - frmraporlar-data
  - id: frmraporlar-copy
    content: Implement clipboard copy functionality for Özet and Aksiyon text boxes
    status: completed
    dependencies:
      - frmraporlar-ui
  - id: frmmail-ui
    content: Expand FrmMail form width to 950px and add AI Assistant panel with all controls (dropdowns, preview, buttons)
    status: completed
  - id: frmmail-ai-logic
    content: Implement AI email template generation logic using Gemini API (btnSablonOner_Click, btnYenidenUret_Click, btnGovdeyeAktar_Click)
    status: completed
    dependencies:
      - gemini-aiservice
      - frmmail-ui
  - id: report-data-formatter
    content: Create ReportDataFormatter.cs service class to format DataTable to structured text for AI (max 50 rows, truncate values)
    status: completed
  - id: unit-tests-pii
    content: Implement unit tests for PiiMaskingService (email, phone, TC, IBAN, name masking)
    status: completed
  - id: unit-tests-prompt
    content: Implement unit tests for PromptBuilder (report summary and email template prompt generation)
    status: completed
  - id: unit-tests-parser
    content: Implement unit tests for AiResponseParser (summary/actions parsing, email parts parsing)
    status: completed
  - id: unit-tests-rate
    content: Implement unit tests for AiRateLimiter (global limit, user limit, wait time calculation)
    status: completed
  - id: integration-tests-gemini
    content: Implement integration tests for AiService with Gemini API (requires GEMINI_API_KEY, mark as Explicit)
    status: completed
    dependencies:
      - gemini-aiservice
      - gemini-env-setup
  - id: functional-tests-raporlar
    content: Implement functional tests for FrmRaporlar AI Summary with Gemini API (happy path, empty report, rate limit, network error, clipboard)
    status: completed
    dependencies:
      - frmraporlar-ai-logic
  - id: functional-tests-mail
    content: Implement functional tests for FrmMail AI Assistant with Gemini API (template generation, body transfer, regeneration, scenario combinations)
    status: completed
    dependencies:
      - frmmail-ai-logic
  - id: security-tests
    content: Implement security tests to verify PII masking in all data flows and log security
    status: completed
  - id: error-handling-gemini
    content: Implement comprehensive error handling with user-friendly messages for all error scenarios including Gemini-specific errors (quota, invalid key, etc.)
    status: completed
    dependencies:
      - frmraporlar-ai-logic
      - frmmail-ai-logic
  - id: feature-flags
    content: Implement feature flag checks in both forms to show/hide AI features based on App.config settings
    status: completed
    dependencies:
      - frmraporlar-ui
      - frmmail-ui
  - id: validation-gemini
    content: Validate all functionality against AI_TEST_SENARYOLARI.md test scenarios and verify Gemini API integration works correctly
    status: completed
    dependencies:
      - frmraporlar-ai-logic
      - frmmail-ai-logic
      - functional-tests-raporlar
      - functional-tests-mail
---

# AI Implementation Plan - operion (Gemini API)

## Overview

This plan implements the complete AI integration for operion Ticari Otomasyon system using **Google Gemini API**. The AI services infrastructure is already implemented in `Application/Services/`. This plan focuses on:

1. **Gemini API Integration**: Update AiService to support Gemini API request/response format
2. **Configuration**: Update App.config for Gemini API settings
3. **UI Integration**: Adding AI features to FrmRaporlar and FrmMail forms
4. **Data Preparation**: Extracting and formatting report data from database
5. **Test Implementation**: Implementing test scenarios from AI_TEST_SENARYOLARI.md
6. **Validation**: Ensuring all features work with Gemini API

## Current State Analysis

### What Exists

- ✅ All AI service classes in `Application/Services/`
- ✅ App.config with AI settings (currently configured for OpenAI)
- ✅ PromptBuilder with ReportSummaryContext and EmailTemplateContext models
- ✅ PII masking service ready
- ✅ Rate limiting and logging infrastructure
- .env and Google Gemini API Key

### What Needs Update

- ⚠️ AiService: Currently supports OpenAI/Azure OpenAI only, needs Gemini API support
- ⚠️ App.config: Needs Gemini API endpoint and model configuration
- ❌ FrmRaporlar: No "AI Özeti" tab or AI summary functionality
- ❌ FrmMail: No AI Assistant panel or email template generation
- ❌ Report data extraction and formatting for AI
- ❌ UI controls for AI features (ProgressBar, MemoEdit, Buttons)
- ❌ Test scenarios implementation

## Gemini API Integration

### Phase 0: Gemini API Support in AiService

#### 0.1 Update AiService.cs for Gemini API

**File**: `Application/Services/AiService.cs`**Changes Required**:

1. **Update BuildRequestBody method** to support Gemini:
```csharp
private object BuildRequestBody(string prompt)
{
    if (_provider.ToLowerInvariant().Contains("gemini"))
    {
        return new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0.7,
                maxOutputTokens = int.Parse(ConfigurationManager.AppSettings["AI_MAX_TOKENS"] ?? "2000"),
                topP = 0.8,
                topK = 40
            },
            systemInstruction = new
            {
                parts = new[]
                {
                    new { text = "Sen Türkçe konuşan bir iş asistanısın. Her zaman Türkçe yanıt verirsin." }
                }
            }
        };
    }
    else if (_provider.ToLowerInvariant().Contains("openai") || _provider.ToLowerInvariant().Contains("azure"))
    {
        // Existing OpenAI/Azure code...
    }
    
    throw new NotSupportedException($"Provider '{_provider}' henüz desteklenmiyor");
}
```




2. **Update CallAiApiAsync method** for Gemini authentication:
```csharp
// Provider'a göre header ve endpoint ekleme
if (_provider.ToLowerInvariant().Contains("gemini"))
{
    // Gemini API key is passed as query parameter
    var endpointWithKey = $"{_endpoint}?key={_apiKey}";
    request = new HttpRequestMessage(HttpMethod.Post, endpointWithKey)
    {
        Content = content
    };
}
else if (_provider.ToLowerInvariant().Contains("openai"))
{
    request.Headers.Add("Authorization", $"Bearer {_apiKey}");
}
// ... existing code
```




3. **Update ParseResponse method** for Gemini response format:
```csharp
private AiResponse ParseResponse(string responseContent)
{
    var json = JObject.Parse(responseContent);
    
    if (_provider.ToLowerInvariant().Contains("gemini"))
    {
        var content = json["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString() ?? "";
        var usageMetadata = json["usageMetadata"];
        
        return new AiResponse
        {
            Content = content,
            PromptTokens = usageMetadata?["promptTokenCount"]?.Value<int>() ?? 0,
            CompletionTokens = usageMetadata?["candidatesTokenCount"]?.Value<int>() ?? 0,
            TotalTokens = usageMetadata?["totalTokenCount"]?.Value<int>() ?? 0,
            Provider = _provider
        };
    }
    else if (_provider.ToLowerInvariant().Contains("openai") || _provider.ToLowerInvariant().Contains("azure"))
    {
        // Existing OpenAI/Azure parsing...
    }
    
    throw new NotSupportedException($"Provider '{_provider}' yanıt formatı desteklenmiyor");
}
```




#### 0.2 Update App.config for Gemini API

**File**: `App.config`**Changes Required**:Update AI configuration section:

```xml
<appSettings>
    <!-- AI Sağlayıcı Yapılandırması - Gemini API -->
    <add key="AI_PROVIDER" value="Gemini" />
    <!-- Gemini API endpoint - model adı endpoint'e dahil -->
    <add key="AI_ENDPOINT" value="https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent" />
    <!-- API Anahtarı - ENV: prefix ile çevre değişkeninden okunabilir -->
    <add key="AI_API_KEY" value="ENV:GEMINI_API_KEY" />
    <!-- Model seçimi (endpoint'te belirtilir, burada referans için) -->
    <add key="AI_MODEL" value="gemini-1.5-flash" />
    <!-- Maksimum token sayısı -->
    <add key="AI_MAX_TOKENS" value="2000" />
    
    <!-- Zaman Aşımı ve Yeniden Deneme -->
    <add key="AI_TIMEOUT_MS" value="30000" />
    <add key="AI_RETRY_COUNT" value="3" />
    
    <!-- Rate Limiting (Hız Sınırlama) -->
    <add key="AI_RATE_LIMIT_GLOBAL" value="30" />
    <add key="AI_RATE_LIMIT_PER_USER" value="10" />
    
    <!-- Loglama Ayarları -->
    <add key="AI_LOGGING_ENABLED" value="true" />
    <add key="AI_LOG_DIRECTORY" value="Logs\AI" />
    
    <!-- Özellik Anahtarları (Feature Flags) -->
    <add key="FEATURE_AI_REPORT_SUMMARY" value="true" />
    <add key="FEATURE_AI_EMAIL_ASSISTANT" value="true" />
    
    <!-- PII Güvenlik Ayarları -->
    <add key="AI_MASK_CUSTOMER_NAMES" value="true" />
    <add key="AI_MASK_PERSONAL_DATA" value="true" />
    <add key="AI_DATA_MINIMIZATION" value="true" />
    
    <!-- Cache Ayarları -->
    <add key="AI_CACHE_ENABLED" value="true" />
    <add key="AI_CACHE_DURATION_MINUTES" value="15" />
</appSettings>
```

**Note**: Gemini API key should be set as environment variable:

```powershell
[Environment]::SetEnvironmentVariable("GEMINI_API_KEY", "your-gemini-api-key-here", "User")
```



#### 0.3 Gemini API Model Options

Available Gemini models:

- `gemini-1.5-flash` - Fast, cost-effective (recommended for this use case)
- `gemini-1.5-pro` - More capable, higher cost
- `gemini-pro` - Previous generation

Endpoint format: `https://generativelanguage.googleapis.com/v1beta/models/{MODEL_NAME}:generateContent`

## Implementation Phases

### Phase 1: FrmRaporlar AI Summary Integration

#### 1.1 UI Design Updates

**File**: `Presentation/Forms/Reports/FrmRaporlar.Designer.cs`Add new TabPage "AI Özeti" to existing TabControl:

- TabPage: `tabPageAiOzet` (Text: "🤖 AI Özeti")
- Controls to add:
- `btnOzetUret`: ModernButton (Primary style, "Özet Üret")
- `lblOzetBaslik`: Label ("Rapor Özeti:")
- `txtOzet`: RichTextBox (ReadOnly, MultiLine, ScrollBars.Vertical)
- `lblAksiyonBaslik`: Label ("Aksiyon Maddeleri:")
- `txtAksiyon`: RichTextBox (ReadOnly, MultiLine, ScrollBars.Vertical)
- `btnKopyalaOzet`: ModernButton (Secondary style, "Panoya Kopyala (Özet)")
- `btnKopyalaAksiyon`: ModernButton (Secondary style, "Panoya Kopyala (Aksiyon)")
- `progressBarAi`: ProgressBar (Style: Marquee, Visible: false)
- `lblStatus`: Label (Status messages, token count, duration)

**Layout**:

- Top section: Button and progress bar
- Left panel: Özet (50% width)
- Right panel: Aksiyon (50% width)
- Bottom: Copy buttons

#### 1.2 Report Data Extraction

**File**: `Presentation/Forms/Reports/FrmRaporlar.cs`Create method `PrepareReportDataForAi(string reportType)`:

- Use `ReportViewerHelper.GetReportSql(reportType)` to get SQL query
- Execute query using `DatabaseService.GetConnection()`
- Format data as text (CSV-like or structured text)
- Limit to 50 rows maximum (per AI_IMPLEMENTASYON_RAPORU.md)
- Truncate column values to 50 characters max
- Apply PII masking using `PiiMaskingService.PrepareReportDataForAi()`

**Data Format Example**:

```javascript
Rapor: Firmalar Raporu
Tarih: 01.01.2025 - 31.12.2025
Toplam Kayıt: 45

Sütunlar: FirmaAd, VergiNo, Telefon, E-posta, Adres
---
[MASKELENMIŞ_FIRMA_001], [KIMLIK_NO], [TELEFON], [EMAIL], [ADRES]
...
```



#### 1.3 AI Summary Logic

**File**: `Presentation/Forms/Reports/FrmRaporlar.cs`Add fields:

```csharp
private readonly AiService _aiService = new AiService();
private readonly PromptBuilder _promptBuilder = new PromptBuilder();
private readonly AiResponseParser _aiParser = new AiResponseParser();
private readonly PiiMaskingService _piiMasking = new PiiMaskingService();
private readonly AiRateLimiter _rateLimiter = new AiRateLimiter();
```

Add async method `btnOzetUret_Click`:

1. Check feature flag: `ConfigurationManager.AppSettings["FEATURE_AI_REPORT_SUMMARY"]`
2. Check if AI is configured: `_aiService.IsConfigured()`
3. Check rate limit: `_rateLimiter.CanMakeRequest(userId)`
4. Get current report type from selected tab
5. Extract and prepare report data
6. Show progress bar, disable button
7. Build prompt: `_promptBuilder.BuildReportSummaryPrompt(context)`
8. Call AI: `await _aiService.SummarizeAsync(prompt)`
9. Parse response: `_aiParser.ParseSummaryAndActions(response.Content)`
10. Display results in txtOzet and txtAksiyon
11. Show status: "Özet başarıyla oluşturuldu (X saniye - Y token)"
12. Handle errors with user-friendly messages

#### 1.4 Copy to Clipboard

Add methods:

- `btnKopyalaOzet_Click`: Copy txtOzet.Text to clipboard
- `btnKopyalaAksiyon_Click`: Copy txtAksiyon.Text to clipboard
- Show success message: "Panoya kopyalandı"

#### 1.5 Error Handling

Handle scenarios:

- AI not configured: "AI servisi yapılandırılmamış. Lütfen App.config'i kontrol edin."
- Rate limit exceeded: "Çok fazla istek gönderildi. Lütfen X saniye bekleyin."
- No report data: "Rapor verisi bulunamadı. Önce bir rapor oluşturun."
- API timeout: "AI yanıtı zaman aşımına uğradı. Lütfen tekrar deneyin."
- Network error: "İnternet bağlantısı hatası. Lütfen bağlantınızı kontrol edin."
- Gemini API specific errors: Handle quota exceeded, invalid API key, etc.

### Phase 2: FrmMail AI Assistant Integration

#### 2.1 UI Design Updates

**File**: `Presentation/Forms/Settings/FrmMail.Designer.cs`Expand form width from 500px to 950px (per AI_IMPLEMENTASYON_RAPORU.md).Add AI Assistant Panel (GroupBox or ModernPanel):

- `grpAiAsistan`: GroupBox (Title: "🤖 AI E-posta Asistanı")
- Controls:
- `cmbScenario`: ComboBox (Label: "Senaryo:", Items: Teklif, Teşekkür, Ödeme Hatırlatma, Teslimat Bilgi, Genel Yanıt)
- `cmbTon`: ComboBox (Label: "Ton:", Items: Resmi, Nötr, Samimi, Acil)
- `cmbUzunluk`: ComboBox (Label: "Uzunluk:", Items: Kısa, Orta, Uzun)
- `cmbKonu`: ComboBox (Label: "Konu Satırı Seç:", Enabled: false, populated after generation)
- `txtOnizleme`: RichTextBox (Label: "Önizleme:", ReadOnly, MultiLine)
- `btnSablonOner`: ModernButton (Primary, "Şablon Öner")
- `btnYenidenUret`: ModernButton (Secondary, "Yeniden Üret", Enabled: false)
- `btnGovdeyeAktar`: ModernButton (Success, "Gövdeye Aktar", Enabled: false)
- `progressBarAi`: ProgressBar (Marquee, Visible: false)
- `lblStatus`: Label (Status messages)

**Layout**: Split form horizontally:

- Left: Existing mail form (500px)
- Right: AI Assistant panel (450px)

#### 2.2 AI Email Template Logic

**File**: `Presentation/Forms/Settings/FrmMail.cs`Add fields:

```csharp
private readonly AiService _aiService = new AiService();
private readonly PromptBuilder _promptBuilder = new PromptBuilder();
private readonly AiResponseParser _aiParser = new AiResponseParser();
private readonly PiiMaskingService _piiMasking = new PiiMaskingService();
private readonly AiRateLimiter _rateLimiter = new AiRateLimiter();
private ParsedEmailTemplate? _currentTemplate;
```

Add async method `btnSablonOner_Click`:

1. Check feature flag: `FEATURE_AI_EMAIL_ASSISTANT`
2. Check AI configuration
3. Check rate limit
4. Get customer reference from txtmailadres (mask if needed)
5. Build context: `EmailTemplateContext` with selected scenario, tone, length
6. Build prompt: `_promptBuilder.BuildEmailTemplatePrompt(context)`
7. Call AI: `await _aiService.GenerateEmailAsync(prompt)`
8. Parse response: `_aiParser.ParseEmailParts(response.Content)`
9. Populate cmbKonu with 3 subject lines
10. Display email body in txtOnizleme
11. Enable "Yeniden Üret" and "Gövdeye Aktar" buttons
12. Show status message

Add method `btnYenidenUret_Click`: Same as btnSablonOner_Click (regenerate)Add method `btnGovdeyeAktar_Click`:

1. Get selected subject from cmbKonu
2. Set txtmailkonu.Text = selected subject
3. Set rchmailmesaj.Text = txtOnizleme.Text
4. Show success message: "E-posta formuna aktarıldı"

#### 2.3 Customer Reference Masking

When building EmailTemplateContext:

- If txtmailadres contains known customer email, mask it
- Use `_piiMasking.MaskCustomerReference(customerName)` if available
- Otherwise use generic "[MÜŞTERİ]" reference

### Phase 3: Test Scenarios Implementation

#### 3.1 Unit Tests Structure

**Create**: `Tests/Application/Services/` folder structure**Files to create**:

- `PiiMaskingServiceTests.cs`
- `PromptBuilderTests.cs`
- `AiResponseParserTests.cs`
- `AiRateLimiterTests.cs`

**Test Framework**: Use NUnit or xUnit (check project preferences)

#### 3.2 Integration Tests

**Create**: `Tests/Integration/` folder**Files**:

- `AiServiceIntegrationTests.cs` (requires Gemini API key, mark as [Explicit])
- `FrmRaporlarAiTests.cs` (UI tests, manual or automated)
- `FrmMailAiTests.cs` (UI tests)

**Note**: Integration tests should use Gemini API key from environment variable.

#### 3.3 Functional Test Checklist

Implement test scenarios from `AI_TEST_SENARYOLARI.md`:**FrmRaporlar Tests**:

- ✅ Test 1.1: Özet Üretme (Happy Path)
- ✅ Test 1.2: Boş Rapor Senaryosu
- ✅ Test 1.3: Rate Limit Aşımı
- ✅ Test 1.4: İnternet Yok Senaryosu
- ✅ Test 1.5: Panoya Kopyalama

**FrmMail Tests**:

- ✅ Test 2.1: Şablon Oluşturma (Happy Path)
- ✅ Test 2.2: Gövdeye Aktarma
- ✅ Test 2.3: Yeniden Üretme
- ✅ Test 2.4: Farklı Senaryo Testleri (5 scenarios × 4 tones × 3 lengths)

#### 3.4 Security Tests

**PII Masking Validation**:

- Verify emails are masked in report data
- Verify phone numbers are masked
- Verify TC/Vergi No are masked
- Verify IBANs are masked
- Verify customer names are masked

**Log Security**:

- Verify API keys are not logged
- Verify PII data is masked in logs

### Phase 4: Data Preparation Helper

#### 4.1 Report Data Formatter

**File**: `Application/Services/ReportDataFormatter.cs` (new)Create class to format report data for AI:

- Method: `FormatReportDataForAi(DataTable data, int maxRows = 50)`
- Convert DataTable to structured text
- Include column headers
- Limit rows and truncate values
- Return formatted string

#### 4.2 Integration with FrmRaporlar

Use `ReportDataFormatter` in `PrepareReportDataForAi()` method to ensure consistent formatting.

### Phase 5: Feature Flag Validation

#### 5.1 Runtime Checks

Both forms should check feature flags before showing AI UI:

- `FEATURE_AI_REPORT_SUMMARY` for FrmRaporlar
- `FEATURE_AI_EMAIL_ASSISTANT` for FrmMail

If flag is false:

- Hide AI tab/panel
- Or show disabled state with message

### Phase 6: Error Messages and User Feedback

#### 6.1 Standardized Messages

Create constants class or resource file for user-facing messages:

- "AI servisi yapılandırılmamış"
- "Çok fazla istek gönderildi. Lütfen {seconds} saniye bekleyin."
- "Rapor verisi bulunamadı"
- "Özet başarıyla oluşturuldu ({duration} saniye - {tokens} token)"
- "Gemini API hatası: {error}" (for Gemini-specific errors)

#### 6.2 Progress Indicators

- Show progress bar during AI calls
- Disable buttons during processing
- Show status messages with token count and duration
- Handle cancellation (if needed)

## Technical Considerations

### Gemini API Specifics

**Endpoint Format**:

- Base: `https://generativelanguage.googleapis.com/v1beta/models/{MODEL}:generateContent`
- API Key: Query parameter `?key=YOUR_API_KEY`
- Method: POST
- Content-Type: application/json

**Request Format**:

```json
{
  "contents": [{
    "parts": [{
      "text": "prompt text here"
    }]
  }],
  "generationConfig": {
    "temperature": 0.7,
    "maxOutputTokens": 2000,
    "topP": 0.8,
    "topK": 40
  },
  "systemInstruction": {
    "parts": [{
      "text": "System instruction text"
    }]
  }
}
```

**Response Format**:

```json
{
  "candidates": [{
    "content": {
      "parts": [{
        "text": "response text here"
      }]
    }
  }],
  "usageMetadata": {
    "promptTokenCount": 100,
    "candidatesTokenCount": 50,
    "totalTokenCount": 150
  }
}
```

**Error Handling**:

- 400: Invalid request (check prompt format)
- 401: Invalid API key
- 403: Quota exceeded or permission denied
- 429: Rate limit exceeded
- 500: Server error

### Async/Await Pattern

- All AI calls must be async
- Use `async void` only for event handlers
- Proper error handling with try-catch
- UI thread updates using `Invoke` if needed

### Rate Limiting

- Check before each AI call
- Show wait time to user if limit exceeded
- Use `AiRateLimiter.GetWaitTime(userId)` for user feedback
- Gemini API has its own rate limits (check Google Cloud Console)

### PII Masking

- Always mask before sending to AI
- Use `PiiMaskingService.PrepareReportDataForAi()` for reports
- Use `PiiMaskingService.MaskCustomerReference()` for customer names

### Logging

- All AI operations logged via `AiLogger`
- Include request type, duration, token count
- Mask PII in logs
- Log Gemini API specific errors

### Configuration

- Read from App.config using `ConfigurationManager.AppSettings`
- Support ENV: prefix for sensitive values (API keys)
- Validate configuration on form load
- Gemini API key must be set as environment variable: `GEMINI_API_KEY`

## Files to Modify

### Existing Files

1. `Application/Services/AiService.cs` - Add Gemini API support (BuildRequestBody, CallAiApiAsync, ParseResponse)
2. `App.config` - Update AI configuration for Gemini API
3. `Presentation/Forms/Reports/FrmRaporlar.cs` - Add AI summary logic
4. `Presentation/Forms/Reports/FrmRaporlar.Designer.cs` - Add AI Özeti tab UI
5. `Presentation/Forms/Settings/FrmMail.cs` - Add AI assistant logic
6. `Presentation/Forms/Settings/FrmMail.Designer.cs` - Add AI assistant panel UI

### New Files to Create

1. `Application/Services/ReportDataFormatter.cs` - Report data formatting helper
2. `Tests/Application/Services/PiiMaskingServiceTests.cs` - Unit tests
3. `Tests/Application/Services/PromptBuilderTests.cs` - Unit tests
4. `Tests/Application/Services/AiResponseParserTests.cs` - Unit tests
5. `Tests/Application/Services/AiRateLimiterTests.cs` - Unit tests
6. `Tests/Integration/AiServiceIntegrationTests.cs` - Integration tests (Gemini API)

## Validation Checklist

### Gemini API Configuration

- [ ] App.config updated with Gemini endpoint and provider
- [ ] GEMINI_API_KEY environment variable set
- [ ] AiService.IsConfigured() returns true
- [ ] Test API call to Gemini succeeds

### FrmRaporlar AI Summary

- [ ] AI Özeti tab appears in TabControl
- [ ] Özet Üret button triggers Gemini API call
- [ ] Progress bar shows during processing
- [ ] Özet and Aksiyon text boxes populated correctly
- [ ] Copy to clipboard works for both
- [ ] Error messages are user-friendly
- [ ] Rate limiting works correctly
- [ ] PII data is masked before sending
- [ ] Feature flag check works

### FrmMail AI Assistant

- [ ] AI Assistant panel appears in form
- [ ] Form width expanded to 950px
- [ ] Scenario, Tone, Length dropdowns work
- [ ] Şablon Öner button generates template via Gemini
- [ ] 3 subject lines appear in dropdown
- [ ] Email body preview shows correctly
- [ ] Gövdeye Aktar copies to main form
- [ ] Yeniden Üret regenerates template
- [ ] Error handling works
- [ ] Feature flag check works

### Tests

- [ ] Unit tests for PII masking pass
- [ ] Unit tests for PromptBuilder pass
- [ ] Unit tests for AiResponseParser pass
- [ ] Unit tests for AiRateLimiter pass
- [ ] Integration tests for AiService pass (with Gemini API key)
- [ ] Functional tests from AI_TEST_SENARYOLARI.md pass

## Dependencies

### Required NuGet Packages

- ✅ Newtonsoft.Json (already in project)
- ✅ System.Configuration.ConfigurationManager (already in project)
- Test framework: NUnit or xUnit (to be added if not present)

### External Dependencies

- **Google Gemini API key** (configured in App.config via ENV:GEMINI_API_KEY)
- Internet connection for Gemini API calls
- Google Cloud account with Gemini API enabled

## Risk Mitigation

### API Key Security

- ✅ ENV: prefix support already implemented
- Document secure key management in user guide
- Gemini API key should never be hardcoded

### Rate Limiting

- ✅ AiRateLimiter already implemented
- Show clear messages when limit exceeded
- Be aware of Gemini API quota limits (check Google Cloud Console)

### Error Handling

- Comprehensive try-catch blocks
- User-friendly error messages
- Technical details in logs only
- Handle Gemini-specific errors (quota, invalid key, etc.)

### Performance

- Limit report data to 50 rows
- Truncate column values
- Show progress indicators
- Async operations to prevent UI freezing
- Gemini API response times may vary

## Success Criteria

1. ✅ AiService supports Gemini API (request/response format)
2. ✅ App.config configured for Gemini API
3. ✅ FrmRaporlar AI Summary tab functional with Gemini
4. ✅ FrmMail AI Assistant panel functional with Gemini
5. ✅ All test scenarios from AI_TEST_SENARYOLARI.md pass
6. ✅ PII masking verified in all data flows
7. ✅ Error handling covers all edge cases (including Gemini-specific)
8. ✅ User experience matches documented behavior
9. ✅ Feature flags work correctly
10. ✅ Logging captures all operations

## Timeline Estimate

- Phase 0 (Gemini API Support): 1 day
- Phase 1 (FrmRaporlar): 2-3 days
- Phase 2 (FrmMail): 2-3 days
- Phase 3 (Tests): 2-3 days
- Phase 4-6 (Helpers & Polish): 1-2 days

**Total**: 8-12 days for complete implementation