using System.Data;
using Microsoft.Data.Sqlite;
using operion.Application.Services;

namespace operion.Application.Services
{
    public class SqlGenerationService
    {
        private readonly AiService _aiService;
        private readonly IDatabaseSchemaService _schemaService;

        public SqlGenerationService(AiService aiService, IDatabaseSchemaService schemaService)
        {
            _aiService = aiService;
            _schemaService = schemaService;
        }

        public async Task<string?> GenerateSqlAsync(string userQuery)
        {
            var schema = _schemaService.GetSchemaDefinition();
            
            var prompt = $@"
SEN BİR SQL UZMANISIN. SADECE SQL SORGUSU ÜRET.
Aşağıdaki veritabanı şemasını kullanarak, kullanıcının sorusunu SQLite formatında bir SQL sorgusuna dönüştür.

Kurallar:
1. SADECE SQL kodunu döndür. Markdown bloğu kullanma (```sql ... ``` YAZMA). 
2. Açıklama metni EKLEME.
3. Tehlikeli komutları (DROP, DELETE, UPDATE, INSERT, ALTER) asla kullanma. Sadece SELECT kullan.
4. Tablo ve kolon isimlerini şemaya birebir uygun yaz.
5. 'SILINDI' kolonu olan tablolarda, silinmiş kayıtları getirme (SILINDI = 0).
6. Kelime benzerliği gerektiren durumlarda 'LIKE' kullan.

Şema:
{schema}

Kullanıcı Sorusu: {userQuery}
SQL Sorgusu:
";

            try
            {
                var response = await _aiService.SummarizeAsync(prompt); // SummarizeAsync sends raw request
                var sql = response.Content.Trim();

                // Basic Cleanup
                if (sql.StartsWith("```sql")) sql = sql.Substring(6);
                if (sql.StartsWith("```")) sql = sql.Substring(3);
                if (sql.EndsWith("```")) sql = sql.Substring(0, sql.Length - 3);
                
                return sql.Trim();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"SQL Generation Error: {ex.Message}");
                return null;
            }
        }

        public bool IsSafeSql(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return false;
            
            var upperSql = sql.ToUpperInvariant();
            if (upperSql.Contains("DELETE ") || 
                upperSql.Contains("UPDATE ") || 
                upperSql.Contains("INSERT ") || 
                upperSql.Contains("DROP ") || 
                upperSql.Contains("ALTER ") || 
                upperSql.Contains("TRUNCATE "))
            {
                return false;
            }

            return true;
        }

        public async Task<string> ExecuteQueryAsync(string sql)
        {
            if (!IsSafeSql(sql)) return "Güvenlik uyarısı: Bu sorgu çalıştırılamaz.";

            try
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    // Connection is already opened in GetConnection
                    // await connection.OpenAsync(); 

                    using (var cmd = new SqliteCommand(sql, connection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var sb = new System.Text.StringBuilder();
                            
                            // Headers
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sb.Append(reader.GetName(i)).Append(" | ");
                            }
                            sb.AppendLine();
                            sb.AppendLine(new string('-', 20));

                            // Rows
                            bool hasRows = false;
                            while (await reader.ReadAsync())
                            {
                                hasRows = true;
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    sb.Append(reader[i]?.ToString() ?? "NULL").Append(" | ");
                                }
                                sb.AppendLine();
                            }

                            if (!hasRows) return "Sorgu sonuç döndürmedi.";
                            
                            return sb.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Sorgu hatası: {ex.Message}";
            }
        }
    }
}
