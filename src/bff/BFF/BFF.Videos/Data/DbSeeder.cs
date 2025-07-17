using BFF.Videos.Data.Entities;

namespace BFF.Videos.Data
{
    public static class DbSeeder
    {
        private static Dictionary<int, string> Categories = new Dictionary<int, string>
        {
            { 1, "Action" },
            { 2, "Adventure" },
            { 3, "Animation" },
            { 4, "Comedy" },
            { 5, "Crime" },
            { 6, "Documentary" },
            { 7, "Drama" },
            { 8, "Fantasy" },
            { 9, "Historical" },
            { 10, "Horror" },
            { 11, "Mystery" },
            { 12, "Musical" },
            { 13, "Romance" },
            { 14, "Sci-Fi" },
            { 15, "Sports" },
            { 16, "Thriller" },
            { 17, "War" },
            { 18, "Western" },
            { 19, "Superhero" },
            { 20, "Family" }
        };

        public static void SeedCategories(AppDbContext context)
        {
            if (!context.Categories.Any())
            {

                foreach (var item in Categories)
                {
                    context.Categories.Add(new Category { Id = item.Key, Name = item.Value });
                }

                context.SaveChanges();
            }
        }
    }
}
