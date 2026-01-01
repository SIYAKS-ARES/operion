using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Personeller tablosu
    /// BLOB desteği: PersonelFoto eklendi
    /// </summary>
    [Table("TBL_PERSONELLER")]
    public class TblPersoneller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonelID { get; set; }
        
        public string? PersonelAd { get; set; }
        
        public string? PersonelSoyad { get; set; }
        
        public string? PersonelTelefon { get; set; }
        
        public string? PersonelTC { get; set; }
        
        public string? PersonelMail { get; set; }
        
        public string? PersonelSehir { get; set; }
        
        public string? PersonelIlce { get; set; }
        
        public string? PersonelAdres { get; set; }
        
        public string? PersonelGorev { get; set; }
        
        /// <summary>
        /// Personel fotoğrafı (BLOB) - .NET 10 için eklendi
        /// </summary>
        public byte[]? PersonelFoto { get; set; }
    }
}

