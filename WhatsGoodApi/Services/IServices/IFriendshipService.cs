using WhatsGoodApi.Models;

namespace WhatsGoodApi.Services.IServices
{
    public interface IFriendshipService
    {
        Task CreateFriendship(FriendRequest request, int userId);
        Task<List<User>> GetAllFriendsForUser(int UserId);
        Task<bool> CheckIfFriends(string UserName, string FriendName);
    }
}
