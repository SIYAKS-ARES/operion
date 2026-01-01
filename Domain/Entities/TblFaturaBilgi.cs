using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Fatura bilgileri tablosu
    /// </summary>
    [Table("TBL_FATURABILGI")]
    public class TblFaturaBilgi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FaturaID { get; set; }
        
        public string? FaturaSeri { get; set; }
        
        public string? FaturaSiraNo { get; set; }
        
        public string? FaturaTarih { get; set; }
        
        public string? FaturaSaat { get; set; }
        
        public string? FaturaVergiDairesi { get; set; }
        
        public string? FaturaAlici { get; set; }
        
        public string? FaturaTeslimEden { get; set; }
        
        public string? FaturaTeslimAlan { get; set; }
        
        // Navigation property
        public virtual ICollection<TblFaturaDetay>? FaturaDetaylar { get; set; }
    }
}

