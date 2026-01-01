using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Notlar tablosu
    /// </summary>
    [Table("TBL_NOTLAR")]
    public class TblNotlar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotID { get; set; }
        
        public string? NotTarih { get; set; }
        
        public string? NotSaat { get; set; }
        
        public string? NotBaslik { get; set; }
        
        public string? NotDetay { get; set; }
        
        public string? NotOlusturan { get; set; }
        
        public string? NotHitap { get; set; }
    }
}

