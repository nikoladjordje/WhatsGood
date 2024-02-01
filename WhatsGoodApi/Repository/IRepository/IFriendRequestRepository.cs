using WhatsGoodApi.Models;

namespace WhatsGoodApi.Repository.IRepository
{
    public interface IFriendRequestRepository : IRepository<FriendRequest>
    {
        Task<FriendRequest> SendFriendRequest(FriendRequest friendRequest);
        Task<FriendRequest> GetFriendRequestBySenderAndRecipient(int SenderId, int RecipientId);
        Task<FriendRequest> GetFriendRequestById(int RequestId, int UserId);
        Task<List<FriendRequest>> GetFriendRequestsByUser(int UserId);
        Task<FriendRequest> AcceptFriendRequest(FriendRequest friendRequest);
        Task<FriendRequest> DeclineFriendRequest(FriendRequest friendRequest);
    }
}
