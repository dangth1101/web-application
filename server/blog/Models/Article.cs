using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class Article
    {
        [Key]
        public required int ArticleId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid AuthorId { get; set; }
        public required User Author { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public required bool IsActive { get; set; }

    }
}