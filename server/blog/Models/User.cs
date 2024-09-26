using System.ComponentModel.DataAnnotations;

namespace blog.Models {
    public class User {
        [Key]
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<Comment> Comments{ get; set; } = new List<Comment>();
        public ICollection<Article> Articles{ get; set; } = new List<Article>();
        public required bool IsActive { get; set; }
        
    }
}