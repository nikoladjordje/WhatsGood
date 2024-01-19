using Azure.Core;
using WhatsGoodApi.DTOs;
using WhatsGoodApi.Models;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.Unit;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly WhatsGoodDbContext _db;
        public UnitOfWork _unitOfWork { get; set; }

        public FriendRequestService(WhatsGoodDbContext db)
        {
            this._db = db;
            this._unitOfWork = new UnitOfWork(db);
        }

        public async Task SendFriendRequest(FriendRequestDTO request)
        {
            if (request != null)
            {
                var requestFound = await this._unitOfWork.FriendRequest.GetFriendRequestBySenderAndRecipient(request.SenderId, request.RecipientId);
                if (requestFound != null)
                {
                    throw new Exception("Friend request already sent.");
                }

                var requestCreated = new FriendRequest(request.SenderId, request.RecipientId, request.IsAccepted, request.Timestamp);
                await _unitOfWork.FriendRequest.Add(requestCreated);
                await _unitOfWork.Save();

            }
        }
        public async Task AcceptFriendRequest(int requestId)
        {
            var request = await this._unitOfWork.FriendRequest.GetFriendRequestById(requestId);
            if (request == null)
            {
                throw new Exception("No such friend request");
            }
            request.IsAccepted = true;
            _unitOfWork.FriendRequest.Update(request);
            await _unitOfWork.Save();
        }

        public async Task DeclineFriendRequest(int requestId)
        {
            var request = await this._unitOfWork.FriendRequest.GetFriendRequestById(requestId);
            if (request == null)
            {
                throw new Exception("No such friend request");
            }
            _unitOfWork.FriendRequest.Delete(request);
            await _unitOfWork.Save();
        }

        public async Task<List<FriendRequest>> GetAllFriendRequestsForUser(int UserId)
        {
            List<FriendRequest> friends = await this._unitOfWork.FriendRequest.GetFriendRequestsByUser(UserId);
            return friends;
        }
        public async Task<bool> CheckIfFriendRequestSent(string UserName, string FriendName)
        {
            var friend1 = await this._unitOfWork.User.GetUserByUsername(UserName);
            var friend2 = await this._unitOfWork.User.GetUserByUsername(FriendName);
            if (friend1 == null || friend2 == null)
            {
                return false;
            }
            var friendsList = await this._unitOfWork.FriendRequest.GetFriendRequestBySenderAndRecipient(friend1.ID, friend2.ID);
            if (friendsList == null)
                return false;
            return true;
        }
    }
}
