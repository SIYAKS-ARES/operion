using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Ürünler tablosu
    /// BLOB desteği: UrunResim eklendi
    /// </summary>
    [Table("TBL_URUNLER")]
    public class TblUrunler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UrunID { get; set; }
        
        public string? UrunAd { get; set; }
        
        public string? UrunMarka { get; set; }
        
        public string? UrunModel { get; set; }
        
        public string? UrunYil { get; set; }
        
        public int? UrunAdet { get; set; }
        
        public double? UrunMaliyet { get; set; }
        
        public double? UrunSatisFiyat { get; set; }
        
        public string? UrunDetay { get; set; }
        
        /// <summary>
        /// Ürün resmi (BLOB) - .NET 10 için eklendi
        /// </summary>
        public byte[]? UrunResim { get; set; }
    }
}

