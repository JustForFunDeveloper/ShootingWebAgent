using Microsoft.EntityFrameworkCore;
using ShootingWebAgent.DataModels.APIModel;

namespace ShootingWebAgent.SQLite
{

    public class DataDbContext : DbContext
    {
        public DbSet<DisagJson> DisagJsons { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Shooter> Shooters { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Object> Objects { get; set; }

        public DataDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
