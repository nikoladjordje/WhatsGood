using WhatsGoodApi.Models;

namespace WhatsGoodApi.Repository.IRepository
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        Task<List<User>> GetFriendsForUser(int UserId);
        Task<Friendship> GetFriendshipByUserAndFriend(int UserId, int FriendId);
        Task<Friendship> CreateFriendship(Friendship friendship);
    }
}
