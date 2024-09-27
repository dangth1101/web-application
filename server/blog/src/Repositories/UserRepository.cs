using blog.DbContexts;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Repositories
{
    public interface IUserRepository
    {
        public Task<ICollection<User>> GetUsers();
        public Task<User?> GetUserById(Guid userId);
        public Task CreateUser(User user);
        public Task UpdateUser(User user);
        public Task SoftDeleteUser(Guid userId);
        public Task HardDeleteUser(Guid userId);
    }

    public class UserRepostiroy : IUserRepository
    {
        private readonly BlogContext _blogContext;

        public UserRepostiroy(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task CreateUser(User user)
        {
            if (user is null)
            {
                throw new Exception("Cannot insert null user");
            }

            _blogContext.Users.Add(user);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            return await _blogContext.Users.SingleOrDefaultAsync(user => user.UserId.Equals(userId));
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _blogContext.Users.ToListAsync();
        }

        public async Task HardDeleteUser(Guid userId)
        {
            User? user = await _blogContext.Users.FindAsync(userId);
            if (user is null)
            {
                throw new Exception($"User with Id {userId} not found!");
            }

            _blogContext.Users.Remove(user);
            await _blogContext.SaveChangesAsync();
        }

        public async Task SoftDeleteUser(Guid userId)
        {
            User? user = await _blogContext.Users.FindAsync(userId);
            if (user is null)
            {
                throw new Exception($"User with Id {userId} not found!");
            }

            user!.IsActive = false;
            await _blogContext.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            User? currentUser = await _blogContext.Users.FindAsync(user.UserId);
            if (currentUser is null)
            {
                throw new Exception($"Article with Id {user.UserId} not found!");
            }

            currentUser.Email = user.Email;
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.ModifiedAt = DateTime.UtcNow;
            await _blogContext.SaveChangesAsync();
        }
    }
}