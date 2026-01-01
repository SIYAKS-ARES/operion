using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Stoklar tablosu
    /// </summary>
    [Table("TBL_STOKLAR")]
    public class TblStoklar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StokID { get; set; }
        
        public string? StokTur { get; set; }
        
        public string? StokAdet { get; set; }
    }
}

