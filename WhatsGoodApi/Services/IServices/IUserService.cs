using WhatsGoodApi.DTOs;
using WhatsGoodApi.Models;

namespace WhatsGoodApi.Services.IServices
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDTO user);
        Task<string> Login(string email, string password);
        Task UpdateProfile(UserUpdateDTO user);
        Task<User> GetUser(string jwt);
        Task<IQueryable<User>> Search(string username, string ownerUsername);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByUserId(int userId);
    }
}
