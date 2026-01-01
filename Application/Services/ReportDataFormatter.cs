using System;
using System.Data;
using System.Text;

namespace operion.Application.Services
{
    /// <summary>
    /// Rapor verilerini AI için formatlar
    /// DataTable'ı yapılandırılmış metin formatına dönüştürür
    /// </summary>
    public static class ReportDataFormatter
    {
        /// <summary>
        /// DataTable'ı AI için formatlanmış metne dönüştürür
        /// </summary>
        /// <param name="data">Formatlanacak DataTable</param>
        /// <param name="maxRows">Maksimum satır sayısı (varsayılan: 50)</param>
        /// <param name="maxColumnLength">Sütun değeri maksimum uzunluğu (varsayılan: 50 karakter)</param>
        /// <returns>Formatlanmış metin</returns>
        public static string FormatReportDataForAi(DataTable data, int maxRows = 50, int maxColumnLength = 50)
        {
            if (data == null || data.Rows.Count == 0)
            {
                return "Rapor verisi bulunamadı.";
            }

            var sb = new StringBuilder();
            
            // Başlık bilgileri
            sb.AppendLine($"Toplam Kayıt: {data.Rows.Count}");
            sb.AppendLine();
            
            // Sütun başlıkları
            var columnNames = new string[data.Columns.Count];
            for (int i = 0; i < data.Columns.Count; i++)
            {
                columnNames[i] = data.Columns[i].ColumnName;
            }
            sb.AppendLine($"Sütunlar: {string.Join(", ", columnNames)}");
            sb.AppendLine("---");
            
            // Veri satırları (maksimum maxRows kadar)
            int rowCount = Math.Min(data.Rows.Count, maxRows);
            for (int i = 0; i < rowCount; i++)
            {
                var row = data.Rows[i];
                var values = new string[data.Columns.Count];
                
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    var value = row.IsNull(j) ? "" : row[j]?.ToString() ?? "";
                    
                    // Maksimum uzunluk kontrolü
                    if (value.Length > maxColumnLength)
                    {
                        value = value.Substring(0, maxColumnLength) + "...";
                    }
                    
                    values[j] = value;
                }
                
                sb.AppendLine(string.Join(", ", values));
            }
            
            if (data.Rows.Count > maxRows)
            {
                sb.AppendLine();
                sb.AppendLine($"... (Toplam {data.Rows.Count} kayıt, ilk {maxRows} kayıt gösteriliyor)");
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// SQLiteDataReader'dan DataTable oluşturur
        /// </summary>
        public static DataTable DataReaderToDataTable(Microsoft.Data.Sqlite.SqliteDataReader reader, int maxRows = 50)
        {
            var dataTable = new DataTable();
            
            // Sütunları oluştur
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataTable.Columns.Add(reader.GetName(i), typeof(string));
            }
            
            // Verileri oku (maksimum maxRows kadar)
            int rowCount = 0;
            while (reader.Read() && rowCount < maxRows)
            {
                var row = dataTable.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader.IsDBNull(i) ? "" : reader.GetValue(i)?.ToString() ?? "";
                }
                dataTable.Rows.Add(row);
                rowCount++;
            }
            
            return dataTable;
        }
    }
}

