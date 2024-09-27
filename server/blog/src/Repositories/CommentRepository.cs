using blog.DbContexts;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Repositories
{
    public interface ICommentRepository
    {
        public Task<ICollection<Comment>> GetComments();
        public Task<Comment?> GetCommentById(int commentId);
        public Task<ICollection<Comment>> GetCommentByAuthor(Guid authorId);
        public Task CreateComment(Comment comment);
        public Task UpdateComment(Comment comment);
        public Task SoftDeleteComment(int commentId);
        public Task HardDeleteComment(int commentId);
    }

    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _blogContext;

        public CommentRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task CreateComment(Comment comment)
        {
            if (comment is null)
            {
                throw new Exception("Cannot insert null comment");
            }

            _blogContext.Comments.Add(comment);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<ICollection<Comment>> GetCommentByAuthor(Guid authorId)
        {
            return await _blogContext.Comments.Where(comment => comment.AuthorId.Equals(authorId)).ToListAsync();
        }

        public async Task<Comment?> GetCommentById(int commentId)
        {
            return await _blogContext.Comments.SingleOrDefaultAsync(comment => comment.CommentId == commentId);
        }

        public async Task<ICollection<Comment>> GetComments()
        {
            return await _blogContext.Comments.ToListAsync();
        }

        public async Task HardDeleteComment(int commentId)
        {
            Comment? comment = await _blogContext.Comments.FindAsync(commentId);
            if (comment is null)
            {
                throw new Exception($"Comment with Id {commentId} not found!");
            }

            _blogContext.Comments.Remove(comment);
            await _blogContext.SaveChangesAsync();
        }

        public async Task SoftDeleteComment(int commentId)
        {
            Comment? comment = await _blogContext.Comments.FindAsync(commentId);
            if (comment is null)
            {
                throw new Exception($"Comment with Id {commentId} not found!");
            }

            comment!.IsActive = false;
            await _blogContext.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            Comment? currentComment = await _blogContext.Comments.FindAsync(comment.CommentId);
            if (currentComment is null)
            {
                throw new Exception($"Comment with Id {comment.CommentId} not found!");
            }

            currentComment.Content = comment.Content;
            currentComment.ModifiedAt = DateTime.UtcNow;
            await _blogContext.SaveChangesAsync();
        }
    }
}