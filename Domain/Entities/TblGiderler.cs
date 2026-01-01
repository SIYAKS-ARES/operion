using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Giderler tablosu
    /// </summary>
    [Table("TBL_GIDERLER")]
    public class TblGiderler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GiderID { get; set; }
        
        public string? GiderAy { get; set; }
        
        public string? GiderYil { get; set; }
        
        public double? GiderElektrik { get; set; }
        
        public double? GiderSu { get; set; }
        
        public double? GiderDogalgaz { get; set; }
        
        public double? GiderInternet { get; set; }
        
        public double? GiderMaaslar { get; set; }
        
        public double? GiderEkstra { get; set; }
        
        public string? GiderNotlar { get; set; }
    }
}

