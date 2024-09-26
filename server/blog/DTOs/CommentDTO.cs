namespace blog.DTOs {
    public class CommentDTO {
        public required int CommentId { get; set; }
        public required string Content { get; set; }
        public required Guid AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}