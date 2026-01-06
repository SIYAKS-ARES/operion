using System;

namespace operion.Core.Models
{
    /// <summary>
    /// TCMB Dviz kuru bilgilerini tasiyan model
    /// </summary>
    public class DovizKuru
    {
        public string Kod { get; set; }
        public string Isim { get; set; }
        public int Birim { get; set; }
        public decimal Alis { get; set; }
        public decimal Satis { get; set; }
        public decimal EfektifAlis { get; set; }
        public decimal EfektifSatis { get; set; }
    }
}
