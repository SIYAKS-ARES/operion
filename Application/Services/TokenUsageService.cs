using System;
using System.IO;
using Newtonsoft.Json;

namespace operion.Application.Services
{
    public class TokenUsageService
    {
        private readonly string _usageFilePath;
        private TokenUsageData _usageData;
        private const int DEFAULT_DAILY_LIMIT = 1000000; // 1 Million Tokens default

        public TokenUsageService()
        {
            try
            {
                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                _usageFilePath = Path.Combine(directory, "token_usage.json");
                LoadUsageData();
            }
            catch (Exception ex)
            {
                // Fallback to in-memory if file access fails
                System.Diagnostics.Debug.WriteLine($"TokenUsageService init error: {ex.Message}");
                _usageData = new TokenUsageData();
            }
        }

        public void TrackUsage(int promptTokens, int completionTokens)
        {
            // Reset if new day
            if (_usageData.LastResetDate.Date < DateTime.Now.Date)
            {
                _usageData.DailyUsedTokens = 0;
                _usageData.LastResetDate = DateTime.Now;
            }

            int total = promptTokens + completionTokens;
            _usageData.DailyUsedTokens += total;
            _usageData.TotalUsedTokens += total;
            
            SaveUsageData();
        }

        public bool IsLimitExceeded()
        {
             // Reset if new day (check before validation)
            if (_usageData.LastResetDate.Date < DateTime.Now.Date)
            {
                _usageData.DailyUsedTokens = 0;
                _usageData.LastResetDate = DateTime.Now;
                SaveUsageData();
                return false;
            }

            return _usageData.DailyUsedTokens >= _usageData.DailyLimit;
        }

        public (int DailyUsed, int DailyLimit, int TotalUsed) GetUsageStats()
        {
            return (_usageData.DailyUsedTokens, _usageData.DailyLimit, _usageData.TotalUsedTokens);
        }

        public void SetDailyLimit(int limit)
        {
            _usageData.DailyLimit = limit;
            SaveUsageData();
        }

        private void LoadUsageData()
        {
            if (File.Exists(_usageFilePath))
            {
                try
                {
                    string json = File.ReadAllText(_usageFilePath);
                    _usageData = JsonConvert.DeserializeObject<TokenUsageData>(json) ?? new TokenUsageData();
                }
                catch
                {
                    _usageData = new TokenUsageData();
                }
            }
            else
            {
                _usageData = new TokenUsageData();
            }
        }

        private void SaveUsageData()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_usageData, Formatting.Indented);
                File.WriteAllText(_usageFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving usage data: {ex.Message}");
            }
        }

        private class TokenUsageData
        {
            public int DailyLimit { get; set; } = DEFAULT_DAILY_LIMIT;
            public int DailyUsedTokens { get; set; } = 0;
            public int TotalUsedTokens { get; set; } = 0;
            public DateTime LastResetDate { get; set; } = DateTime.Now;
        }
    }
}
