using Microsoft.EntityFrameworkCore;
using operion.Domain.Entities;
using System.IO;

namespace operion.Infrastructure.Data
{
    /// <summary>
    /// Ticari Otomasyon veritabanı context'i
    /// Entity Framework Core kullanarak veri erişimi sağlar
    /// </summary>
    public class TicariOtomasyonDbContext : DbContext
    {
        public DbSet<TblAdmin> TblAdmin => Set<TblAdmin>();
        public DbSet<TblIller> TblIller => Set<TblIller>();
        public DbSet<TblIlceler> TblIlceler => Set<TblIlceler>();
        public DbSet<TblFirmalar> TblFirmalar => Set<TblFirmalar>();
        public DbSet<TblBankalar> TblBankalar => Set<TblBankalar>();
        public DbSet<TblPersoneller> TblPersoneller => Set<TblPersoneller>();
        public DbSet<TblMusteriler> TblMusteriler => Set<TblMusteriler>();
        public DbSet<TblUrunler> TblUrunler => Set<TblUrunler>();
        public DbSet<TblStoklar> TblStoklar => Set<TblStoklar>();
        public DbSet<TblFaturaBilgi> TblFaturaBilgi => Set<TblFaturaBilgi>();
        public DbSet<TblFaturaDetay> TblFaturaDetay => Set<TblFaturaDetay>();
        public DbSet<TblFirmaHareketler> TblFirmaHareketler => Set<TblFirmaHareketler>();
        public DbSet<TblMusteriHareketler> TblMusteriHareketler => Set<TblMusteriHareketler>();
        public DbSet<TblGiderler> TblGiderler => Set<TblGiderler>();
        public DbSet<TblNotlar> TblNotlar => Set<TblNotlar>();
        public DbSet<TblKodlar> TblKodlar => Set<TblKodlar>();

        /// <summary>
        /// DbContext yapılandırması
        /// SQLite veritabanı bağlantısını ayarlar
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = Application.Services.DatabaseService.GetDatabasePath();
                
                // Data klasörünü oluştur
                string? dataDir = Path.GetDirectoryName(dbPath);
                if (!string.IsNullOrEmpty(dataDir) && !Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }

                // ARM Windows 11 uyumlu SQLite connection string
                string connectionString = $"Data Source={dbPath};Mode=ReadWriteCreate;Cache=Shared;";
                
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        /// <summary>
        /// Model yapılandırması
        /// Tablo isimleri ve ilişkileri ayarlar
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TBL_ADMIN tablosu için key yapılandırması
            // SQLite'de composite key veya string key için özel yapılandırma gerekebilir
            modelBuilder.Entity<TblAdmin>()
                .HasKey(e => e.KullaniciAd);

            // TBL_KODLAR tablosu için key yapılandırması
            // Bu tablo primary key olmadan oluşturulmuş, key olmayabilir
            modelBuilder.Entity<TblKodlar>()
                .HasNoKey();

            // Foreign key ilişkileri (EF Core tarafından otomatik algılanır ama açıkça belirtilebilir)
            modelBuilder.Entity<TblBankalar>()
                .HasOne(b => b.Firma)
                .WithMany(f => f.Bankalar)
                .HasForeignKey(b => b.FirmaID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TblFaturaDetay>()
                .HasOne(fd => fd.FaturaBilgi)
                .WithMany(fb => fb.FaturaDetaylar)
                .HasForeignKey(fd => fd.FaturaID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TblFirmaHareketler>()
                .HasOne(fh => fh.Urun)
                .WithMany()
                .HasForeignKey(fh => fh.UrunID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TblFirmaHareketler>()
                .HasOne(fh => fh.Personel)
                .WithMany()
                .HasForeignKey(fh => fh.PersonelID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TblFirmaHareketler>()
                .HasOne(fh => fh.Firma)
                .WithMany()
                .HasForeignKey(fh => fh.FirmaID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TblMusteriHareketler>()
                .HasOne(mh => mh.Urun)
                .WithMany()
                .HasForeignKey(mh => mh.UrunID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TblMusteriHareketler>()
                .HasOne(mh => mh.Personel)
                .WithMany()
                .HasForeignKey(mh => mh.PersonelID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TblMusteriHareketler>()
                .HasOne(mh => mh.Musteri)
                .WithMany()
                .HasForeignKey(mh => mh.MusteriID)
                .OnDelete(DeleteBehavior.SetNull);

            // BLOB kolonları için özel yapılandırma (EF Core otomatik algılar ama açıkça belirtilebilir)
            modelBuilder.Entity<TblUrunler>()
                .Property(u => u.UrunResim)
                .HasColumnType("BLOB");

            modelBuilder.Entity<TblPersoneller>()
                .Property(p => p.PersonelFoto)
                .HasColumnType("BLOB");

            modelBuilder.Entity<TblFirmalar>()
                .Property(f => f.FirmaLogo)
                .HasColumnType("BLOB");
        }
    }
}

