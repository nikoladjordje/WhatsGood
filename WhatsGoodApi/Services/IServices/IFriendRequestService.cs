using WhatsGoodApi.DTOs;
using WhatsGoodApi.Models;

namespace WhatsGoodApi.Services.IServices
{
    public interface IFriendRequestService
    {
        Task SendFriendRequest(FriendRequestDTO request);
        Task<FriendRequest> AcceptFriendRequest(int requestId, int userId);
        Task DeclineFriendRequest(int requestId, int userId);
        Task<bool> CheckIfFriendRequestSent(int UserId, int FriendId);
        Task<List<FriendRequest>> GetAllFriendRequestsForUser(int UserId);
    }
}
