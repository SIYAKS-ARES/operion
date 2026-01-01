using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// İlçeler tablosu
    /// </summary>
    [Table("TBL_ILCELER")]
    public class TblIlceler
    {
        [Key]
        public int ID { get; set; }
        
        public string? ILCE { get; set; }
        
        public int SEHIR { get; set; }
        
        [ForeignKey("SEHIR")]
        public virtual TblIller? Sehir { get; set; }
    }
}

