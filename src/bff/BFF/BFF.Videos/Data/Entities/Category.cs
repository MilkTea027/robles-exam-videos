using System.ComponentModel.DataAnnotations;

namespace BFF.Videos.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}