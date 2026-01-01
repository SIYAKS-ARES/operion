using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Kodlar tablosu
    /// </summary>
    [Table("TBL_KODLAR")]
    public class TblKodlar
    {
        [Key]
        public string? FIRMAKOD1 { get; set; }
        
        public string? FIRMAKOD2 { get; set; }
        
        public string? FIRMAKOD3 { get; set; }
    }
}

