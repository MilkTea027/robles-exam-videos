using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BFF.Videos.Data.Entities
{
    public class Video
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Thumbnail { get; set; }

        public string? Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public string? File { get; set; }

        public long Size { get; set; }

        public required Category Category { get; set; }
    }
}
