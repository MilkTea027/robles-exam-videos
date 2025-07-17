using BFF.Videos.Data.Entities;
using BFF.Videos.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureVideo();
            modelBuilder.ConfigureCategory();
        }
    }
}
