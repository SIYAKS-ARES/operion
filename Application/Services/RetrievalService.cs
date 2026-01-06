using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace operion.Application.Services
{
    public class RetrievalService
    {
        private readonly RagService _ragService;
        private readonly AiService _aiService;
        private readonly ReRankingService _reRankingService;

        public RetrievalService(RagService ragService, AiService aiService)
        {
            _ragService = ragService;
            _aiService = aiService;
            _reRankingService = new ReRankingService(aiService);
        }

        /// <summary>
        /// Kullanıcı sorgusunu alıp en alakalı döküman parçalarını getirir.
        /// </summary>
        public async Task<List<string>> RetrieveContextAsync(string userQuery, bool useQueryExpansion = true)
        {
            var searchQueries = new List<string> { userQuery };

            // 1. Sorgu Genişletme (Query Expansion)
            if (useQueryExpansion)
            {
                string expandedQuery = await ExpandQueryAsync(userQuery);
                if (!string.IsNullOrEmpty(expandedQuery))
                {
                    // Genişletilmiş sorguyu da aramaya ekle (veya ayrı ayrı ara birleştir)
                    // Şimdilik sadece logluyoruz veya basitçe primary query olarak kullanabiliriz.
                    // Strateji: Orijinal sorgu + Genişletilmiş varyasyonlarla ara
                    searchQueries.Add(expandedQuery);
                }
            }

            // 2. Arama (Vektör)
            // Fetch more candidates for re-ranking (e.g., 10 instead of 3-4)
            string finalSearchQuery = string.Join(" ", searchQueries);
            
            // Initial Retrieval
            var vectorResults = await _ragService.SearchAsync(finalSearchQuery, limit: 10);

            // 3. Re-ranking (Cross-Encoder / LLM Listwise)
            // Re-rank top 10 down to top 3
            var reRankedResults = await _reRankingService.ReRankAsync(userQuery, vectorResults, topK: 3);

            return reRankedResults;
        }

        /// <summary>
        /// LLM kullanarak sorguyu genişletir / eş anlamlılarını bulur.
        /// </summary>
        private async Task<string> ExpandQueryAsync(string query)
        {
            try
            {
                var prompt = $@"
<system_role>
Sen bir arama asistanısın. Kullanıcının sorgusunu veritabanında daha iyi aramak için anahtar kelimelerle zenginleştir.
</system_role>
<input>
{query}
</input>
<instructions>
<instruction>Sadece alternatif anahtar kelimeleri ve eş anlamlıları yaz.</instruction>
<instruction>Orijinal soruyu değiştirme, tamamlayıcı terimler ekle.</instruction>
<instruction>Tek satırda, virgülle ayrılmış olarak döndür.</instruction>
<instruction>Örnek: 'Fatura iptali' -> 'fatura silme, iade faturası, iptal işlemi, muhasebe kaydı silme'</instruction>
</instructions>
";
                var response = await _aiService.SummarizeAsync(prompt); // SummarizeAsync'i genel çağrı için kullanıyoruz
                return response?.Content?.Trim() ?? "";
            }
            catch
            {
                // Genişletme fail olursa orijinal sorguyla devam et
                return "";
            }
        }
    }
}
