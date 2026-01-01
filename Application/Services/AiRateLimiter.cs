using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace operion.Application.Services
{
    /// <summary>
    /// AI servisi için rate limiting (hız sınırlama) yönetimi
    /// </summary>
    public class AiRateLimiter
    {
        private static readonly object _lock = new object();
        private static Dictionary<string, Queue<DateTime>> _userRequests = new Dictionary<string, Queue<DateTime>>();
        private static Queue<DateTime> _globalRequests = new Queue<DateTime>();
        
        private readonly int _maxRequestsPerMinute;
        private readonly int _maxRequestsPerMinutePerUser;
        private readonly TimeSpan _windowDuration = TimeSpan.FromMinutes(1);

        public AiRateLimiter()
        {
            _maxRequestsPerMinute = int.Parse(ConfigurationManager.AppSettings["AI_RATE_LIMIT_GLOBAL"] ?? "30");
            _maxRequestsPerMinutePerUser = int.Parse(ConfigurationManager.AppSettings["AI_RATE_LIMIT_PER_USER"] ?? "10");
        }

        /// <summary>
        /// İstek yapılabilir mi kontrol eder
        /// </summary>
        public bool CanMakeRequest(string? userId = null)
        {
            lock (_lock)
            {
                var now = DateTime.Now;
                
                // Global limit kontrolü
                CleanOldRequests(_globalRequests, now);
                if (_globalRequests.Count >= _maxRequestsPerMinute)
                {
                    return false;
                }
                
                // Kullanıcı bazlı limit kontrolü (opsiyonel)
                if (!string.IsNullOrEmpty(userId))
                {
                    if (!_userRequests.ContainsKey(userId))
                    {
                        _userRequests[userId] = new Queue<DateTime>();
                    }
                    
                    CleanOldRequests(_userRequests[userId], now);
                    if (_userRequests[userId].Count >= _maxRequestsPerMinutePerUser)
                    {
                        return false;
                    }
                }
                
                return true;
            }
        }

        /// <summary>
        /// İsteği kaydeder
        /// </summary>
        public void RecordRequest(string? userId = null)
        {
            lock (_lock)
            {
                var now = DateTime.Now;
                
                // Global kayıt
                _globalRequests.Enqueue(now);
                
                // Kullanıcı bazlı kayıt
                if (!string.IsNullOrEmpty(userId))
                {
                    if (!_userRequests.ContainsKey(userId))
                    {
                        _userRequests[userId] = new Queue<DateTime>();
                    }
                    _userRequests[userId].Enqueue(now);
                }
            }
        }

        /// <summary>
        /// Ne kadar süre beklemesi gerektiğini hesaplar
        /// </summary>
        public TimeSpan GetWaitTime(string? userId = null)
        {
            lock (_lock)
            {
                var now = DateTime.Now;
                
                // Global limit kontrolü
                CleanOldRequests(_globalRequests, now);
                if (_globalRequests.Count >= _maxRequestsPerMinute && _globalRequests.Count > 0)
                {
                    var oldestRequest = _globalRequests.Peek();
                    var waitTime = (oldestRequest + _windowDuration) - now;
                    if (waitTime > TimeSpan.Zero)
                    {
                        return waitTime;
                    }
                }
                
                // Kullanıcı bazlı limit kontrolü
                if (!string.IsNullOrEmpty(userId) && _userRequests.ContainsKey(userId))
                {
                    CleanOldRequests(_userRequests[userId], now);
                    if (_userRequests[userId].Count >= _maxRequestsPerMinutePerUser && _userRequests[userId].Count > 0)
                    {
                        var oldestRequest = _userRequests[userId].Peek();
                        var waitTime = (oldestRequest + _windowDuration) - now;
                        if (waitTime > TimeSpan.Zero)
                        {
                            return waitTime;
                        }
                    }
                }
                
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Eski istekleri temizler (sliding window)
        /// </summary>
        private void CleanOldRequests(Queue<DateTime> requests, DateTime now)
        {
            while (requests.Count > 0 && (now - requests.Peek()) > _windowDuration)
            {
                requests.Dequeue();
            }
        }

        /// <summary>
        /// Kullanıcı istatistiklerini döndürür
        /// </summary>
        public RateLimitStats GetStats(string? userId = null)
        {
            lock (_lock)
            {
                var now = DateTime.Now;
                CleanOldRequests(_globalRequests, now);
                
                var stats = new RateLimitStats
                {
                    GlobalRequestCount = _globalRequests.Count,
                    GlobalLimit = _maxRequestsPerMinute,
                    WindowDuration = _windowDuration
                };
                
                if (!string.IsNullOrEmpty(userId) && _userRequests.ContainsKey(userId))
                {
                    CleanOldRequests(_userRequests[userId], now);
                    stats.UserRequestCount = _userRequests[userId].Count;
                    stats.UserLimit = _maxRequestsPerMinutePerUser;
                }
                
                return stats;
            }
        }

        /// <summary>
        /// Tüm rate limit verilerini temizler (test için)
        /// </summary>
        public void Reset()
        {
            lock (_lock)
            {
                _globalRequests.Clear();
                _userRequests.Clear();
            }
        }
    }

    /// <summary>
    /// Rate limit istatistik modeli
    /// </summary>
    public class RateLimitStats
    {
        public int GlobalRequestCount { get; set; }
        public int GlobalLimit { get; set; }
        public int UserRequestCount { get; set; }
        public int UserLimit { get; set; }
        public TimeSpan WindowDuration { get; set; }
        
        public bool IsGlobalLimitReached => GlobalRequestCount >= GlobalLimit;
        public bool IsUserLimitReached => UserRequestCount >= UserLimit;
        public int GlobalRemainingRequests => Math.Max(0, GlobalLimit - GlobalRequestCount);
        public int UserRemainingRequests => Math.Max(0, UserLimit - UserRequestCount);
    }
}

