using WhatsGoodApi.DTOs;
using WhatsGoodApi.Models;

namespace WhatsGoodApi.Services.IServices
{
    public interface IFriendRequestService
    {
        Task SendFriendRequest(FriendRequestDTO request);
        Task AcceptFriendRequest(int requestId);
        Task DeclineFriendRequest(int requestId);
        Task<bool> CheckIfFriendRequestSent(string UserName, string FriendName);
        Task<List<FriendRequest>> GetAllFriendRequestsForUser(int UserId);
    }
}
