using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Bankalar tablosu
    /// </summary>
    [Table("TBL_BANKALAR")]
    public class TblBankalar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankaID { get; set; }
        
        public string? BankaAd { get; set; }
        
        public string? BankaSube { get; set; }
        
        public string? BankaIBAN { get; set; }
        
        public string? BankaHesapNo { get; set; }
        
        public string? BankaYetkili { get; set; }
        
        public string? BankaTarih { get; set; }
        
        public string? BankaHesapTuru { get; set; }
        
        public int? FirmaID { get; set; }
        
        public string? BankaIl { get; set; }
        
        public string? BankaIlce { get; set; }
        
        public string? BankaTelefon { get; set; }
        
        [ForeignKey("FirmaID")]
        public virtual TblFirmalar? Firma { get; set; }
    }
}

