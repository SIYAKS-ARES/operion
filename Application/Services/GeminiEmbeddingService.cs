using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace operion.Application.Services
{
    /// <summary>
    /// Google Gemini API için özel Semantic Kernel Embedding servisi
    /// </summary>
    public class GeminiEmbeddingService : ITextEmbeddingGenerationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _modelId;
        private readonly IReadOnlyDictionary<string, object?> _attributes = new Dictionary<string, object?>();

        public IReadOnlyDictionary<string, object?> Attributes => _attributes;

        public GeminiEmbeddingService(string apiKey, string modelId = "models/embedding-001", HttpClient? httpClient = null)
        {
            _apiKey = apiKey;
            _modelId = modelId;
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<IList<ReadOnlyMemory<float>>> GenerateEmbeddingsAsync(IList<string> data, Kernel? kernel = null, CancellationToken cancellationToken = default)
        {
            var embeddings = new List<ReadOnlyMemory<float>>();

            // Gemini batch limit is usually 100 or less depending on payload size. 
            // We'll process in batches of 20 to be safe.
            int batchSize = 20;
            
            for (int i = 0; i < data.Count; i += batchSize)
            {
                var batch = data.Skip(i).Take(batchSize).ToList();
                var batchEmbeddings = await GenerateBatchEmbeddingsAsync(batch, cancellationToken);
                embeddings.AddRange(batchEmbeddings);
            }

            return embeddings;
        }

        private async Task<IList<ReadOnlyMemory<float>>> GenerateBatchEmbeddingsAsync(IList<string> batch, CancellationToken cancellationToken)
        {
            var endpoint = $"https://generativelanguage.googleapis.com/v1beta/{_modelId}:batchEmbedContents?key={_apiKey}";

            var requests = batch.Select(text => new
            {
                model = _modelId,
                content = new
                {
                    parts = new[] { new { text = text } }
                }
            }).ToArray();

            var requestBody = new { requests = requests };
            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try 
            {
                response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                     if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        // Rate limit hit - return empty to skip RAG context but keep chat alive
                        System.Diagnostics.Debug.WriteLine($"WARNING: Gemini Embedding Rate Limit Hit (429). Skipping context retrieval.");
                        return CreateEmptyEmbeddings(batch.Count);
                    }

                    var error = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new HttpRequestException($"Gemini Embedding API Error: {response.StatusCode} - {error}");
                }
            }
            catch (Exception ex) when (ex.Message.Contains("429") || ex.Message.Contains("TooManyRequests"))
            {
                 // Safety catch if exception bubbles up elsewhere
                 System.Diagnostics.Debug.WriteLine($"WARNING: Gemini Embedding Rate Limit Hit (Exception). Skipping context retrieval.");
                 return CreateEmptyEmbeddings(batch.Count);
            }

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            var jsonResponse = JObject.Parse(responseString);
            
            var result = new List<ReadOnlyMemory<float>>();
            
            if (jsonResponse["embeddings"] is JArray embeddingArray)
            {
                foreach (var item in embeddingArray)
                {
                    var values = item["values"]?.ToObject<float[]>();
                    if (values != null)
                    {
                        result.Add(new ReadOnlyMemory<float>(values));
                    }
                    else
                    {
                        // Fallback or error? Empty embedding?
                        result.Add(ReadOnlyMemory<float>.Empty);
                    }
                }
            }

            return result;
        }

        private IList<ReadOnlyMemory<float>> CreateEmptyEmbeddings(int count)
        {
            var list = new List<ReadOnlyMemory<float>>();
            // 768 is a typical dimension for Gemini embeddings, but exact size doesn't matter if we just want to bypass SearchAsync finding nothing
            // Actually, if we return zeros, cosine similarity will be 0, so no results will be found. This is exactly what we want (no context).
            var emptyVector = new float[768]; 
            for (int i = 0; i < count; i++)
            {
                list.Add(new ReadOnlyMemory<float>(emptyVector));
            }
            return list;
        }
    }
}
