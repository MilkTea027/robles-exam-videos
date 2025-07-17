using BFF.Videos.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Data.Extensions
{
    public static class CategoryEntityExtension
    {
        public static void ConfigureCategory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}