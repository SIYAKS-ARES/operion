using System.ComponentModel.DataAnnotations;

namespace operion.Domain.Entities
{
    /// <summary>
    /// Admin kullanıcı bilgileri tablosu
    /// </summary>
    public class TblAdmin
    {
        [Key]
        public string? KullaniciAd { get; set; }
        
        public string? KullaniciSifre { get; set; }
    }
}

