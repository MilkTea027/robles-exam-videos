using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BFF.Videos.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}