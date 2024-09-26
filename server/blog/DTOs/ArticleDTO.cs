namespace blog.DTOs
{
    public class ArticleDTO
    {
        public required int BlogId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid AuthorId { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}