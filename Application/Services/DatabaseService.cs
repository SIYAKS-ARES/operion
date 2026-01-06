using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace operion.Application.Services
{
    /// <summary>
    /// Veritabanı bağlantı ve yönetim servisi
    /// System.Data.SQLite'den Microsoft.Data.Sqlite'e güncellenmiştir (.NET 10)
    /// </summary>
    public class DatabaseService
    {
        private static string dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "TicariOtomasyon.db");
        
        /// <summary>
        /// SQL script dosyasının yolunu bulur
        /// Önce build output dizininde arar, yoksa kaynak dizinde arar
        /// </summary>
        private static string GetSqlScriptPath()
        {
            // 1. Önce build output dizininde ara (Infrastructure altinda)
            string outputPath = Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Data", "DB", "TicariOtomasyon_SQLite.sql");
            if (File.Exists(outputPath))
            {
                return outputPath;
            }

            // 2. Eski yolda da ara (Geriye donuk uyumluluk veya farkli kopyalama ayarlari icin)
            string legacyPath = Path.Combine(AppContext.BaseDirectory, "DB", "TicariOtomasyon_SQLite.sql");
            if (File.Exists(legacyPath))
            {
                return legacyPath;
            }
            
            // 3. Kaynak dizininde ara (proje kök dizini) - Development ortamı için
            // AppContext.BaseDirectory genellikle bin\Debug\net10.0-windows\ gibi bir yol
            // Proje köküne çıkmak için 3-4 seviye yukarı
            string? projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            if (!string.IsNullOrEmpty(projectRoot))
            {
                string sourcePath = Path.Combine(projectRoot, "operion", "Infrastructure", "Data", "DB", "TicariOtomasyon_SQLite.sql");
                if (File.Exists(sourcePath))
                {
                    return sourcePath;
                }
            }
            
            // Bulunamazsa varsayılan olarak output path'i döndür (hata mesajı için)
            return outputPath;
        }
        
        private static string sqlScriptPath => GetSqlScriptPath();

        /// <summary>
        /// SQLite veritabanı bağlantısı oluşturur
        /// </summary>
        /// <returns>SqliteConnection nesnesi</returns>
        public static SqliteConnection GetConnection()
        {
            // Data klasörünü oluştur
            string? dataDir = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrEmpty(dataDir) && !Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            // Microsoft.Data.Sqlite connection string formatı
            // ARM Windows 11 uyumlu
            string connectionString = $"Data Source={dbPath};Mode=ReadWriteCreate;Cache=Shared;";
            
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Veritabanını ilk kez oluşturur ve tabloları kurar
        /// </summary>
        public static void InitializeDatabase()
        {
            // Data klasörünü oluştur
            string dataDir = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrEmpty(dataDir) && !Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            // DB klasörünü oluştur (SQL script için)
            string dbDir = Path.GetDirectoryName(sqlScriptPath);
            if (!string.IsNullOrEmpty(dbDir) && !Directory.Exists(dbDir))
            {
                Directory.CreateDirectory(dbDir);
            }

            // SQL script dosyası yoksa hata ver
            if (!File.Exists(sqlScriptPath))
            {
                throw new FileNotFoundException($"SQL script dosyası bulunamadı: {sqlScriptPath}");
            }

            // Veritabanı dosyası varsa tabloları ve VIEW'ları kontrol et
            bool dbExists = File.Exists(dbPath);
            if (dbExists)
            {
                // Tabloların var olup olmadığını kontrol et
                using (var connection = GetConnection())
                {
                    try
                    {
                        using (var cmd = new SqliteCommand(
                            "SELECT name FROM sqlite_master WHERE type='table' AND name='TBL_ADMIN'", 
                            connection))
                        {
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                // Tablo var, VIEW'ları kontrol et
                                EnsureViews(connection);
                                return;
                            }
                        }
                    }
                    catch
                    {
                        // Hata durumunda devam et, tabloları oluştur
                    }
                }
            }

            // SQLite dosyası otomatik oluşturulacak (CreateFile gerekmez)
            using (var connection = GetConnection())
            {
                // SQL script'ini oku ve çalıştır
                string sqlScript = File.ReadAllText(sqlScriptPath);

                // Script'i daha akıllıca parse et - VIEW'lar çok satırlı olabilir
                // Basit yaklaşım: ';' ile ayır ama çok satırlı komutları koru
                var commands = new List<string>();
                var currentCommand = new System.Text.StringBuilder();
                
                string[] lines = sqlScript.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    
                    // Yorum satırlarını atla
                    if (trimmedLine.StartsWith("--", StringComparison.Ordinal))
                    {
                        continue;
                    }
                    
                    // Boş satırları atla
                    if (string.IsNullOrWhiteSpace(trimmedLine))
                    {
                        continue;
                    }
                    
                    currentCommand.AppendLine(line);
                    
                    // Komut sonu - ';' ile bitiyorsa komutu tamamla
                    if (trimmedLine.EndsWith(";", StringComparison.Ordinal))
                    {
                        string cmd = currentCommand.ToString().Trim();
                        if (!string.IsNullOrWhiteSpace(cmd))
                        {
                            // Son ';' karakterini kaldır (SQLite için gerekli değil ama temiz olması için)
                            if (cmd.EndsWith(";"))
                            {
                                cmd = cmd.Substring(0, cmd.Length - 1).Trim();
                            }
                            if (!string.IsNullOrWhiteSpace(cmd))
                            {
                                commands.Add(cmd);
                            }
                        }
                        currentCommand.Clear();
                    }
                }
                
                // Kalan komut varsa ekle (son komut ';' ile bitmemiş olabilir)
                if (currentCommand.Length > 0)
                {
                    string cmd = currentCommand.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(cmd))
                    {
                        if (cmd.EndsWith(";"))
                        {
                            cmd = cmd.Substring(0, cmd.Length - 1).Trim();
                        }
                        if (!string.IsNullOrWhiteSpace(cmd))
                        {
                            commands.Add(cmd);
                        }
                    }
                }

                // Komutları çalıştır
                foreach (string command in commands)
                {
                    string trimmedCommand = command.Trim();
                    
                    if (string.IsNullOrWhiteSpace(trimmedCommand))
                    {
                        continue;
                    }

                    try
                    {
                        using (var cmd = new SqliteCommand(trimmedCommand, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqliteException ex)
                    {
                        // Bazı hatalar normal olabilir (örn: view/tablo zaten var)
                        if (!ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase) &&
                            !ex.Message.Contains("duplicate column", StringComparison.OrdinalIgnoreCase))
                        {
                            System.Diagnostics.Debug.WriteLine($"SQL Error: {ex.Message}");
                            System.Diagnostics.Debug.WriteLine($"Command: {trimmedCommand.Substring(0, Math.Min(200, trimmedCommand.Length))}...");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Beklenmeyen hataları logla
                        System.Diagnostics.Debug.WriteLine($"Unexpected SQL Error: {ex.Message}");
                    }
                }
                
                // VIEW'ları kontrol et ve eksikse oluştur
                EnsureViews(connection);
            }
        }

        /// <summary>
        /// Gerekli VIEW'ların var olup olmadığını kontrol eder, yoksa oluşturur
        /// </summary>
        private static void EnsureViews(SqliteConnection connection)
        {
            try
            {
                // BankaBilgileri VIEW'ını kontrol et
                using (var cmd = new SqliteCommand(
                    "SELECT name FROM sqlite_master WHERE type='view' AND name='BankaBilgileri'", 
                    connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        // VIEW yok, oluştur
                        string createViewSql = @"
                            CREATE VIEW BankaBilgileri AS
                            SELECT 
                                b.BankaID, 
                                b.BankaAd,
                                b.BankaIl,
                                b.BankaIlce,
                                b.BankaYetkili,
                                b.BankaTelefon,
                                b.BankaTarih,
                                b.BankaHesapNo,
                                b.BankaHesapTuru,
                                b.BankaSube,
                                b.BankaIBAN,
                                f.FirmaAd,
                                f.FirmaID
                            FROM TBL_BANKALAR as b
                            LEFT JOIN TBL_FIRMALAR as f 
                            ON b.FirmaID = f.FirmaID";
                        
                        using (var createCmd = new SqliteCommand(createViewSql, connection))
                        {
                            createCmd.ExecuteNonQuery();
                            System.Diagnostics.Debug.WriteLine("BankaBilgileri VIEW oluşturuldu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"VIEW oluşturulurken hata: {ex.Message}");
                // Kritik değil, devam et
            }
        }

        /// <summary>
        /// Veritabanı dosya yolunu döndürür
        /// </summary>
        public static string GetDatabasePath()
        {
            return dbPath;
        }

        /// <summary>
        /// TBL_ADMIN tablosunun var olup olmadığını kontrol eder, yoksa oluşturur
        /// </summary>
        private static void EnsureAdminTable()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Tablo var mı kontrol et
                    using (var cmd = new SqliteCommand(
                        "SELECT name FROM sqlite_master WHERE type='table' AND name='TBL_ADMIN'", 
                        connection))
                    {
                        var result = cmd.ExecuteScalar();
                        
                        // Tablo yoksa oluştur
                        if (result == null)
                        {
                            using (var createCmd = new SqliteCommand(
                                "CREATE TABLE IF NOT EXISTS TBL_ADMIN (" +
                                "KullaniciAd TEXT, " +
                                "KullaniciSifre TEXT" +
                                ")", 
                                connection))
                            {
                                createCmd.ExecuteNonQuery();
                                System.Diagnostics.Debug.WriteLine("TBL_ADMIN tablosu oluşturuldu.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TBL_ADMIN tablosu oluşturulurken hata: {ex.Message}");
                throw; // Kritik hata, yukarı fırlat
            }
        }

        /// <summary>
        /// Admin kullanıcısı yoksa varsayılan admin kullanıcısını ekler
        /// </summary>
        public static void EnsureDefaultAdmin()
        {
            try
            {
                // Önce tablonun var olduğundan emin ol
                EnsureAdminTable();
                
                using (var connection = GetConnection())
                {
                    // Admin kullanıcısı var mı kontrol et
                    using (var cmd = new SqliteCommand(
                        "SELECT COUNT(*) FROM TBL_ADMIN WHERE KullaniciAd = 'admin'", 
                        connection))
                    {
                        var count = Convert.ToInt32(cmd.ExecuteScalar());
                        
                        // Admin kullanıcısı yoksa ekle
                        if (count == 0)
                        {
                            using (var insertCmd = new SqliteCommand(
                                "INSERT INTO TBL_ADMIN (KullaniciAd, KullaniciSifre) VALUES ('admin', 'admin')", 
                                connection))
                            {
                                insertCmd.ExecuteNonQuery();
                                System.Diagnostics.Debug.WriteLine("Varsayılan admin kullanıcısı eklendi.");
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                // Tablo yoksa tekrar dene
                if (ex.Message.Contains("no such table", StringComparison.OrdinalIgnoreCase))
                {
                    System.Diagnostics.Debug.WriteLine("Tablo bulunamadı, yeniden oluşturuluyor...");
                    try
                    {
                        EnsureAdminTable();
                        EnsureDefaultAdmin(); // Tekrar dene
                    }
                    catch
                    {
                        throw new Exception($"Veritabanı tablosu oluşturulamadı: {ex.Message}", ex);
                    }
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Admin kullanıcısı eklenirken hata: {ex.Message}");
                throw; // Hata yukarı fırlatılsın ki kullanıcı görebilsin
            }
        }
    }
}

