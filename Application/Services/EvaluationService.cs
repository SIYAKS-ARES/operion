using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace operion.Application.Services
{
    public class EvaluationService
    {
        private readonly RagService _ragService;
        private readonly RetrievalService _retrievalService;

        public EvaluationService(RagService ragService, RetrievalService retrievalService)
        {
            _ragService = ragService;
            _retrievalService = retrievalService;
        }

        public async Task<EvaluationReport> EvaluateAsync(string datasetPath)
        {
            var report = new EvaluationReport();
            
            if (!File.Exists(datasetPath))
            {
                report.Summary = "Dataset not found.";
                return report;
            }

            var json = File.ReadAllText(datasetPath);
            var dataset = JsonConvert.DeserializeObject<List<GoldenData>>(json);

            if (dataset == null) return report;

            foreach (var item in dataset)
            {
                var result = new EvaluationResult { QuestionId = item.Id, Question = item.Question };
                
                // 1. Context Retrieval Evaluation (Recall/Precision proxy)
                var contexts = await _retrievalService.RetrieveContextAsync(item.Question);
                string combinedContext = string.Join(" ", contexts);
                
                int contextHits = 0;
                foreach (var keyword in item.ExpectedContextKeywords)
                {
                    if (combinedContext.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        contextHits++;
                }

                result.ContextPrecisionScore = item.ExpectedContextKeywords.Count > 0 
                    ? (double)contextHits / item.ExpectedContextKeywords.Count 
                    : 0;

                // 2. Generation (Simulated Answer Check - requires LLM to generate answer first)
                // For this metric, we skip full LLM generation to save tokens/time and focus on Retrieval Quality first.
                // Or we can assume 'contexts' should contain the expected answer keywords.
                
                // Let's check if the retrieved context *contains* the answer keywords (Retrieval Recall)
                int answerHits = 0;
                foreach (var keyword in item.ExpectedAnswerKeywords)
                {
                    if (combinedContext.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        answerHits++;
                }
                
                result.RetrievalRecallScore = item.ExpectedAnswerKeywords.Count > 0
                    ? (double)answerHits / item.ExpectedAnswerKeywords.Count
                    : 0;

                report.Results.Add(result);
            }

            // Calculate overall metrics
            if (report.Results.Count > 0)
            {
                report.AverageContextPrecision = report.Results.Average(r => r.ContextPrecisionScore);
                report.AverageRetrievalRecall = report.Results.Average(r => r.RetrievalRecallScore);
            }
            
            report.Summary = $"Evaluation Complete. Processed {report.Results.Count} items.";
            return report;
        }
    }

    public class GoldenData
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";
        [JsonProperty("question")]
        public string Question { get; set; } = "";
        [JsonProperty("expected_answer_keywords")]
        public List<string> ExpectedAnswerKeywords { get; set; } = new();
        [JsonProperty("expected_context_keywords")]
        public List<string> ExpectedContextKeywords { get; set; } = new();
    }

    public class EvaluationResult
    {
        public string QuestionId { get; set; } = "";
        public string Question { get; set; } = "";
        public double ContextPrecisionScore { get; set; }
        public double RetrievalRecallScore { get; set; }
    }

    public class EvaluationReport
    {
        public List<EvaluationResult> Results { get; set; } = new();
        public double AverageContextPrecision { get; set; }
        public double AverageRetrievalRecall { get; set; }
        public string Summary { get; set; } = "";
    }
}
