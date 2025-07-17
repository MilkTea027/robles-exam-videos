using BFF.Videos.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Data.Extensions
{
    public static class VideoEntityExtension
    {
        public static void ConfigureVideo(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Thumbnail);
                entity.Property(e => e.Description);
                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.File);
                entity.Property(e => e.Size);

                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Videos)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
