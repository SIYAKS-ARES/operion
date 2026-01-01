using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Fatura detay tablosu
    /// </summary>
    [Table("TBL_FATURADETAY")]
    public class TblFaturaDetay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FaturaUrunID { get; set; }
        
        public string? UrunAd { get; set; }
        
        public int? Miktar { get; set; }
        
        public double? Fiyat { get; set; }
        
        public double? Tutar { get; set; }
        
        public int? FaturaID { get; set; }
        
        [ForeignKey("FaturaID")]
        public virtual TblFaturaBilgi? FaturaBilgi { get; set; }
    }
}

