using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Sqlite;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;

namespace operion.Application.Services
{
    /// <summary>
    /// RAG (Retrieval-Augmented Generation) operasyonlarını yöneten servis.
    /// Semantic Kernel üzerine kuruludur.
    /// </summary>
    public class RagService
    {
        private ISemanticTextMemory _memory;
        private Kernel _kernel;
        private readonly string _apiKey;
        private readonly string _persistDirectory;
        private const string COLLECTION_NAME = "operion_docs";

        public RagService(string? persistDirectory = null, bool useVolatile = false, string? apiKey = null, ITextEmbeddingGenerationService? embeddingService = null)
        {
            _apiKey = apiKey ?? GetApiKey();
            
            if (!useVolatile)
            {
                // Use provided directory or default to App_Data
                _persistDirectory = persistDirectory ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "rag_store");
                if (!Directory.Exists(_persistDirectory))
                {
                    Directory.CreateDirectory(_persistDirectory);
                }
            }
            else
            {
                _persistDirectory = ""; // Not used
            }

            // Initialization is now explicit via InitializeAsync
        }

        public async Task InitializeAsync(bool useVolatile = false, ITextEmbeddingGenerationService? embeddingServiceOverride = null)
        {
            // 1. Embedding Servisi
            var embeddingService = embeddingServiceOverride ?? new GeminiEmbeddingService(_apiKey);

            // 2. Memory Store
            try 
            {
                if (useVolatile)
                {
                    // In-Memory SQLite - Pass ":memory:" directly as filename
                    var store = await SqliteMemoryStore.ConnectAsync(":memory:");
                    await BuildKernelAsync(embeddingService, store);
                }
                else
                {
                    // Persistent SQLite
                    if (!Directory.Exists(_persistDirectory)) Directory.CreateDirectory(_persistDirectory);

                    string dbPath = Path.Combine(_persistDirectory, "rag_vectors.db");
                    
                    // Pre-create file to ensure it exists, avoiding "unable to open" if lib uses Open mode
                    if (!File.Exists(dbPath))
                    {
                        try { File.Create(dbPath).Dispose(); } catch { }
                    }

                    // Pass clean path, library handles connection string construction
                    var store = await SqliteMemoryStore.ConnectAsync(dbPath);
                    await BuildKernelAsync(embeddingService, store);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Primary memory store failed: {ex.Message}. Falling back to volatile.");
                // Fallback to volatile
                try 
                {
                    var store = await SqliteMemoryStore.ConnectAsync(":memory:");
                    await BuildKernelAsync(embeddingService, store);
                }
                catch(Exception fallbackEx)
                {
                    // If even memory fails, rethrow basic info
                    throw new Exception($"Kritik Başlatma Hatası: {ex.Message} -> Fallback: {fallbackEx.Message}");
                }
            }
        }

        private async Task BuildKernelAsync(ITextEmbeddingGenerationService embeddingService, IMemoryStore store)
        {
            var memoryBuilder = new MemoryBuilder();
            memoryBuilder.WithTextEmbeddingGeneration(embeddingService);
            memoryBuilder.WithMemoryStore(store);
            
            _memory = memoryBuilder.Build();
            
            // 3. Kernel
            var builder = Kernel.CreateBuilder();
            _kernel = builder.Build();
        }

        /// <summary>
        /// Bir metni embedding alarak hafızaya kaydeder
        /// </summary>
        public async Task SaveInformationAsync(string id, string text, string title, string? additionalMetadata = null)
        {
            string description = additionalMetadata ?? "";
            
            if (_memory == null) return;

            await _memory.SaveInformationAsync(
                collection: COLLECTION_NAME,
                text: text,
                id: id,
                description: title, // Title'ı description olarak saklıyoruz
                additionalMetadata: description
            );
        }

        /// <summary>
        /// Semantic search yapar
        /// </summary>
        public async Task<List<string>> SearchAsync(string query, int limit = 3)
        {
            if (_memory == null)
            {
                // Fallback or empty if not initialized
                System.Diagnostics.Debug.WriteLine("RagService: Memory not initialized.");
                return new List<string>();
            }

            var results = _memory.SearchAsync(
                collection: COLLECTION_NAME,
                query: query,
                limit: limit,
                minRelevanceScore: 0.5 // Eşik değer
            );

            var matches = new List<string>();
            await foreach (var result in results)
            {
                matches.Add($"{result.Metadata.Description}\n{result.Metadata.Text}");
            }
            return matches;
        }

        // --- Helper Methods (Config Reading - AiService'den uyarlandı) ---

        private string GetApiKey()
        {
            string apiKeyConfig = ConfigurationManager.AppSettings["AI_API_KEY"] ?? "";
            if (apiKeyConfig.StartsWith("ENV:", StringComparison.Ordinal))
            {
                string envVarName = apiKeyConfig.Substring(4);
                string envValue = Environment.GetEnvironmentVariable(envVarName);
                if (!string.IsNullOrEmpty(envValue)) return envValue;
                
                string envFileValue = ReadFromEnvFile(envVarName);
                if (!string.IsNullOrEmpty(envFileValue)) return envFileValue;
            }
            return apiKeyConfig;
        }

        private string ReadFromEnvFile(string keyName)
        {
            try
            {
                string envPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
                if (File.Exists(envPath))
                {
                    var lines = File.ReadAllLines(envPath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('=', 2);
                        if (parts.Length == 2 && parts[0].Trim() == keyName)
                        {
                            return parts[1].Trim().Trim('"', '\'');
                        }
                    }
                }
            }
            catch { }
            return "";
        }
    }
}
