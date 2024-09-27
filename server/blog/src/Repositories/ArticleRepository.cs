using blog.DbContexts;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Repositories
{
    public interface IArticleRepository
    {
        public Task<ICollection<Article>> GetArticles();
        public Task<Article?> GetArticleById(int articleId);
        public Task<ICollection<Article>> GetArticleByAuthor(Guid authorId);
        public Task CreateArticle(Article article);
        public Task UpdateArticle(Article article);
        public Task SoftDeleteArticle(int articleId);
        public Task HardDeleteArticle(int articleId);
    }

    public class ArticleRepository : IArticleRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task CreateArticle(Article article)
        {
            if (article is null)
            {
                throw new Exception("Cannot insert null article");
            }

            _blogContext.Articles.Add(article);
            await _blogContext.SaveChangesAsync();
        }

        public async Task SoftDeleteArticle(int articleId)
        {
            Article? article = await _blogContext.Articles.FindAsync(articleId);
            if (article is null)
            {
                throw new Exception($"Article with Id {articleId} not found!");
            }

            article!.IsActive = false;
            await _blogContext.SaveChangesAsync();
        }

        public async Task<ICollection<Article>> GetArticleByAuthor(Guid authorId)
        {
            return await _blogContext.Articles.Where(article => article.AuthorId.Equals(authorId)).ToListAsync();
        }

        public async Task<Article?> GetArticleById(int articleId)
        {
            return await _blogContext.Articles.SingleOrDefaultAsync(article => article.ArticleId == articleId);
        }

        public async Task<ICollection<Article>> GetArticles()
        {
            return await _blogContext.Articles.ToListAsync();
        }

        public async Task HardDeleteArticle(int articleId)
        {
            Article? article = await _blogContext.Articles.FindAsync(articleId);
            if (article is null)
            {
                throw new Exception($"Article with Id {articleId} not found!");
            }

            _blogContext.Articles.Remove(article);
            await _blogContext.SaveChangesAsync();
        }

        public async Task UpdateArticle(Article article)
        {
            Article? currentArticle = await _blogContext.Articles.FindAsync(article.ArticleId);
            if (currentArticle is null)
            {
                throw new Exception($"Article with Id {article.ArticleId} not found!");
            }

            currentArticle.Title = article.Title;
            currentArticle.Content = article.Content;
            currentArticle.ModifiedAt = DateTime.UtcNow;
            currentArticle.Tags = article.Tags;
            await _blogContext.SaveChangesAsync();
        }
    }
}