using WhatsGoodApi.Models;

namespace WhatsGoodApi.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(User user);
        Task<User> Create(User user);
        Task<IQueryable<User>> GetUsersByUsername(string username, string ownerUsername);
    }
}
