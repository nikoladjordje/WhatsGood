using WhatsGoodApi.Models;

namespace WhatsGoodApi.Services.IServices
{
    public interface IFriendshipService
    {
        Task CreateFriendship(int requestId);
        Task<List<Friendship>> GetAllFriendsForUser(int UserId);
        Task<bool> CheckIfFriends(string UserName, string FriendName);
    }
}
