using blog.DbContexts;
using blog.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace blog.Repositories
{
    public interface IArticleRepository
    {
        public Task<ICollection<Article>> GetArticles();
        public Task<Article> GetArticleById(int ArticleId);
        public Task<ICollection<Article>> GetArticleByAuthor(Guid AuthorId);
        public Task CreateArticle(Article article);
        public Task UpdateArticle(Article article);
        public Task DeleteArticle(int ArticleId);
        public Task RemoveArticle(int ArticleId);
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

        public async Task DeleteArticle(int ArticleId)
        {
            Article? article = await _blogContext.Articles.FindAsync(ArticleId);
            if (article is null)
            {
                throw new Exception($"Article with Id {ArticleId} not found!");
            }

            article!.IsActive = false;
            await _blogContext.SaveChangesAsync();
        }

        public async Task<ICollection<Article>> GetArticleByAuthor(Guid AuthorId)
        {
            return
                await _blogContext.Articles.Where(article => article.AuthorId.Equals(AuthorId)).ToListAsync()
                ?? throw new Exception($"Failed to get article by user {AuthorId}");
        }

        public async Task<Article> GetArticleById(int ArticleId)
        {
            return
                await _blogContext.Articles.SingleOrDefaultAsync(article => article.ArticleId == ArticleId)
                ?? throw new Exception($"Article with Id {ArticleId} not found!");
        }

        public async Task<ICollection<Article>> GetArticles()
        {
            return await _blogContext.Articles.ToListAsync();
        }

        public async Task RemoveArticle(int ArticleId)
        {
            Article? article = await _blogContext.Articles.FindAsync(ArticleId);
            if (article is null)
            {
                throw new Exception($"Article with Id {ArticleId} not found!");
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