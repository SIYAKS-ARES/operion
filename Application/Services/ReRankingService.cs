using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace operion.Application.Services
{
    public class ReRankingService
    {
        private readonly AiService _aiService;

        public ReRankingService(AiService aiService)
        {
            _aiService = aiService;
        }

        public async Task<List<string>> ReRankAsync(string query, List<string> documents, int topK = 3)
        {
            if (documents == null || documents.Count == 0) return new List<string>();
            if (documents.Count <= topK) return documents; // No need to re-rank if fewer than K

            // Prepare the prompt for Listwise Re-ranking
            var prompt = new System.Text.StringBuilder();
            prompt.AppendLine("Sen uzman bir arama sonuçları derecelendirme asistanısın.");
            prompt.AppendLine("Aşağıdaki sorgu ve döküman parçalarını incele.");
            prompt.AppendLine("Dökümanları sorguya olan ilgisine göre EN İYİDEN EN KÖTÜYE doğru sırala.");
            prompt.AppendLine("SADECE sıralanmış döküman ID'lerini (index numaralarını) virgülle ayırarak yaz. Başka hiçbir metin yazma.");
            prompt.AppendLine();
            prompt.AppendLine($"Sorgu: {query}");
            prompt.AppendLine();
            prompt.AppendLine("Dökümanlar:");

            for (int i = 0; i < documents.Count; i++)
            {
                // Truncate doc content slightly to save tokens if needed, but for re-ranking full context is better.
                // Replace newlines to keep format clean
                string cleanContent = documents[i].Replace("\r", " ").Replace("\n", " ");
                if (cleanContent.Length > 300) cleanContent = cleanContent.Substring(0, 300) + "...";
                
                prompt.AppendLine($"[{i}] {cleanContent}");
            }

            prompt.AppendLine();
            prompt.AppendLine("Sıralama (Örnek: 1, 0, 2):");

            try
            {
                var response = await _aiService.SummarizeAsync(prompt.ToString());
                var rankingStr = response.Content.Trim();
                
                // Parse indices
                var indices = new List<int>();
                var matches = Regex.Matches(rankingStr, @"\d+");
                foreach (Match match in matches)
                {
                    if (int.TryParse(match.Value, out int idx))
                    {
                        if (idx >= 0 && idx < documents.Count && !indices.Contains(idx))
                        {
                            indices.Add(idx);
                        }
                    }
                }

                // Construct re-ranked list
                var reRankedDocs = new List<string>();
                foreach (var idx in indices)
                {
                    reRankedDocs.Add(documents[idx]);
                }

                // Add any missing docs at the end (fallback)
                for (int i = 0; i < documents.Count; i++)
                {
                    if (!indices.Contains(i))
                    {
                        reRankedDocs.Add(documents[i]);
                    }
                }

                return reRankedDocs.Take(topK).ToList();
            }
            catch (Exception ex)
            {
                // Fallback: return original top K
                Console.WriteLine($"Re-ranking Error: {ex.Message}");
                return documents.Take(topK).ToList();
            }
        }
    }
}
