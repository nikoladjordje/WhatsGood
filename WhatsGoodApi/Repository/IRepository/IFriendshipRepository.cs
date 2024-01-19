using WhatsGoodApi.Models;

namespace WhatsGoodApi.Repository.IRepository
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        Task<List<Friendship>> GetFriendshipByUser(int UserId);
        Task<Friendship> GetFriendshipByUserAndFriend(int UserId, int FriendId);
    }
}
