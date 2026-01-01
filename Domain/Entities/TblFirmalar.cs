using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Firmalar tablosu
    /// BLOB desteği: FirmaLogo eklendi
    /// </summary>
    [Table("TBL_FIRMALAR")]
    public class TblFirmalar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FirmaID { get; set; }
        
        public string? FirmaAd { get; set; }
        
        public string? FirmaYetkiliGorev { get; set; }
        
        public string? FirmaYetkiliAdSoyad { get; set; }
        
        public string? FirmaYetkiliTC { get; set; }
        
        public string? FirmaSektor { get; set; }
        
        public string? FirmaTelefon1 { get; set; }
        
        public string? FirmaTelefon2 { get; set; }
        
        public string? FirmaTelefon3 { get; set; }
        
        public string? FirmaMail { get; set; }
        
        public string? FirmaFax { get; set; }
        
        public string? FirmaSehir { get; set; }
        
        public string? FirmaIlce { get; set; }
        
        public string? FirmaAdres { get; set; }
        
        public string? FirmaVergiDairesi { get; set; }
        
        public string? FirmaOzelKod { get; set; }
        
        public string? FirmaOzelKod2 { get; set; }
        
        public string? FirmaOzelKod3 { get; set; }
        
        /// <summary>
        /// Firma logosu (BLOB) - .NET 10 için eklendi
        /// </summary>
        public byte[]? FirmaLogo { get; set; }
        
        // Navigation property
        public virtual ICollection<TblBankalar>? Bankalar { get; set; }
    }
}

