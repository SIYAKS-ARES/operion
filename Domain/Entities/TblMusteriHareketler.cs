using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Müşteri hareketleri tablosu
    /// </summary>
    [Table("TBL_MUSTERIHAREKETLER")]
    public class TblMusteriHareketler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HareketID { get; set; }
        
        public int? UrunID { get; set; }
        
        public int? Adet { get; set; }
        
        public int? PersonelID { get; set; }
        
        public int? MusteriID { get; set; }
        
        public double? Fiyat { get; set; }
        
        public double? Toplam { get; set; }
        
        public int? FaturaID { get; set; }
        
        public string? Tarih { get; set; }
        
        public string? Notlar { get; set; }
        
        [ForeignKey("UrunID")]
        public virtual TblUrunler? Urun { get; set; }
        
        [ForeignKey("PersonelID")]
        public virtual TblPersoneller? Personel { get; set; }
        
        [ForeignKey("MusteriID")]
        public virtual TblMusteriler? Musteri { get; set; }
        
        [ForeignKey("FaturaID")]
        public virtual TblFaturaBilgi? FaturaBilgi { get; set; }
    }
}

