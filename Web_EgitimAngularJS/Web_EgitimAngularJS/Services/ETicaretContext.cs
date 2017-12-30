using System.Data.Entity;

namespace Web_EgitimAngularJS.Services
{
    public class ETicaretContext : DbContext
    {
        public ETicaretContext() : base("eticaretDB") { }
        public DbSet<Models.Iletisim> Iletisim { get; set; }
        public DbSet<Models.Kategoriler> Kategoriler { get; set; }
        public DbSet<Models.Tedarikciler> Tedarikciler { get; set; }
        public DbSet<Models.Urunler> Urunler { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Iletisim>().ToTable("Iletisim");
            modelBuilder.Entity<Models.Iletisim>().HasKey(i => i.TabloID);

            modelBuilder.Entity<Models.Kategoriler>().ToTable("Kategoriler");
            modelBuilder.Entity<Models.Kategoriler>().HasKey(i => i.TabloID);

            modelBuilder.Entity<Models.Tedarikciler>().ToTable("Tedarikciler");
            modelBuilder.Entity<Models.Tedarikciler>().HasKey(i => i.TabloID);

            modelBuilder.Entity<Models.Urunler>().ToTable("Urunler");
            modelBuilder.Entity<Models.Urunler>().HasKey(i => i.TabloID);
        }
    }
}