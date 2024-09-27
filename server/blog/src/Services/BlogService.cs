using blog.DTOs;
using blog.Models;
using blog.Repositories;

namespace blog.Service
{
    public interface IBlogService
    {
        public Task<ICollection<ArticleDTO>> GetArticles();
        public Task<ICollection<UserDTO>> GetUsers();
        public Task<ICollection<CommentDTO>> GetComments();
        public Task<Guid?> CreateUser(UserDTO userDTO);
        public Task<UserDTO> GetUserById(Guid userId);
    }

    public class BlogService : IBlogService
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ICommentRepository _commentRepository;

        public BlogService(IUserRepository userRepository, IArticleRepository articleRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
        }

        public async Task<Guid?> CreateUser(UserDTO userDTO)
        {
            try
            {
                await _userRepository.CreateUser(new User()
                {
                    UserId = userDTO.UserId,
                    Username = "Tester1",
                    Password = "Tester1",
                    Email = userDTO.Email,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    CreatedAt = userDTO.CreatedAt,
                    ModifiedAt = userDTO.ModifiedAt,
                    IsActive = true,
                });

                return userDTO.UserId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }

        }

        public async Task<ICollection<ArticleDTO>> GetArticles()
        {
            var articles = await _articleRepository.GetArticles();
            return articles.Select(article => new ArticleDTO()
            {
                ArticleId = article.ArticleId,
                Title = article.Title,
                Content = article.Content,
                AuthorId = article.AuthorId,
                CreatedAt = article.CreatedAt,
                PublishedAt = article.PublishedAt,
                ModifiedAt = article.ModifiedAt,
                Tags = article.Tags!.Split(';').ToList()
            }).ToList();
        }

        public async Task<ICollection<CommentDTO>> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            return comments.Select(comment => new CommentDTO()
            {
                CommentId = comment.CommentId,
                Content = comment.Content,
                AuthorId = comment.AuthorId,
                CreatedAt = comment.CreatedAt,
                ModifiedAt = comment.ModifiedAt,
            }).ToList();
        }

        public async Task<UserDTO> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return new UserDTO()
            {
                UserId = user!.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                ModifiedAt = user.ModifiedAt,
            };
        }

        public async Task<ICollection<UserDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return users.Select(user => new UserDTO()
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                ModifiedAt = user.ModifiedAt,
            }).ToList();
        }


    }
}