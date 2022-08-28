using AppCoreV2.DataAccess.Configs;
using DataAccess.Entities;
using DataAccess.Entities.Hesap;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class AykaParfumContext : DbContext
    {
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Kampanya> Kampanyalar { get; set; }
        public DbSet<UrunKampanya> UrunKampanyalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<KullaniciDetay> KullaniciDetaylar { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Rol> Roller { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
            string connectionString = @"server=.\SQLEXPRESS;database=AykaParfum;user id=sa;password=sa;multipleactiveresultsets=true;";

            if (!string.IsNullOrWhiteSpace(ConnectionConfig.ConnectionString))
                connectionString = ConnectionConfig.ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many iliþkiler 
            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Kategori)
                .WithMany(k => k.Urunler)
                .HasForeignKey(u => u.KategoriId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Marka)
                .WithMany(m => m.Urunler)
                .HasForeignKey(u => u.MarkaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Urun>()
                .HasMany(u => u.UrunKampanyalar)
                .WithOne(uk => uk.Urun)
                .HasForeignKey(uk => uk.UrunId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Kampanya>()
                .HasMany(k => k.UrunKampanyalar)
                .WithOne(uk => uk.Kampanya)
                .HasForeignKey(uk => uk.KampanyaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UrunKampanya>()
                .HasKey(uk => new { uk.UrunId, uk.KampanyaId });


            modelBuilder.Entity<Kullanici>()
                .HasOne(kullanici => kullanici.Rol)
                .WithMany(rol => rol.Kullanicilar)
                .HasForeignKey(kullanici => kullanici.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetay>()
                .HasOne(kullaniciDetay => kullaniciDetay.Kullanici)
                .WithOne(kullanici => kullanici.KullaniciDetay)
                .HasForeignKey<KullaniciDetay>(kullaniciDetay => kullaniciDetay.KullaniciId) 
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetay>()
                .HasOne(kullaniciDetay => kullaniciDetay.Ulke)
                .WithMany(ulke => ulke.KullaniciDetaylar)
                .HasForeignKey(kullaniciDetay => kullaniciDetay.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetay>()
                .HasOne(kullaniciDetay => kullaniciDetay.Sehir)
                .WithMany(sehir => sehir.KullaniciDetaylar)
                .HasForeignKey(kullaniciDetay => kullaniciDetay.SehirId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sehir>()
                .HasOne(sehir => sehir.Ulke)
                .WithMany(ulke => ulke.Sehirler)
                .HasForeignKey(sehir => sehir.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
