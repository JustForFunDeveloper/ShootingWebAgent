using Microsoft.EntityFrameworkCore;
using ShootingWebAgent.DataModels;

namespace ShootingWebAgent.SQLite
{

    public class DataDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Team> Teams { get; set; }
        
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
            modelBuilder.Entity<Team>()
                .HasIndex(team => team.TeamHashId).IsUnique();
        }
    }
}
