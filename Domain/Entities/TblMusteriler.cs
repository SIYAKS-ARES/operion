using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Müşteriler tablosu
    /// </summary>
    [Table("TBL_MUSTERILER")]
    public class TblMusteriler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MusteriID { get; set; }
        
        public string? MusteriAd { get; set; }
        
        public string? MusteriSoyad { get; set; }
        
        public string? MusteriTelefon { get; set; }
        
        public string? MusteriTelefon2 { get; set; }
        
        public string? MusteriTC { get; set; }
        
        public string? MusteriMail { get; set; }
        
        public string? MusteriSehir { get; set; }
        
        public string? MusteriIlce { get; set; }
        
        public string? MusteriAdres { get; set; }
        
        public string? MusteriVergiDaire { get; set; }
    }
}

