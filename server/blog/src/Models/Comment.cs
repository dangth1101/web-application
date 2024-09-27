using System.ComponentModel.DataAnnotations;

namespace blog.Models {
    public class Comment {
        [Key]
        public required int CommentId { get; set; }
        public required string Content { get; set; }
        public required Guid AuthorId { get; set; }
        public required User Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public required bool IsActive { get; set; }
    }
}