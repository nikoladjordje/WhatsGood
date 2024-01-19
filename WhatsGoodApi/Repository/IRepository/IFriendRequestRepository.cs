using WhatsGoodApi.Models;

namespace WhatsGoodApi.Repository.IRepository
{
    public interface IFriendRequestRepository : IRepository<FriendRequest>
    {
        Task<FriendRequest> GetFriendRequestBySenderAndRecipient(int SenderId, int RecipientId);
        Task<FriendRequest> GetFriendRequestById(int RequestId);
        Task<List<FriendRequest>> GetFriendRequestsByUser(int UserId);
    }
}
