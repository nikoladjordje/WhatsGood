using Azure.Core;
using Microsoft.Extensions.Logging.Abstractions;
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
        public IUnitOfWork _unitOfWork { get; set; }

        public FriendRequestService(WhatsGoodDbContext db, IUnitOfWork unitOfWork)
        {
            this._db = db;
            this._unitOfWork = unitOfWork;
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
                await _unitOfWork.FriendRequest.SendFriendRequest(requestCreated);
                await _unitOfWork.Save();

            }
        }
        public async Task<FriendRequest> AcceptFriendRequest(int requestId, int userId)
        {
            var request = await this._unitOfWork.FriendRequest.GetFriendRequestById(requestId, userId);

            if (request == null)
            {
                throw new Exception("No such friend request");
            }

            var friendRequestAccepted = await this._unitOfWork.FriendRequest.AcceptFriendRequest(request);
            try
            {
                this._unitOfWork.FriendRequest.Update(friendRequestAccepted);
                friendRequestAccepted.Sender = null;
                friendRequestAccepted.Recipient = null;
                await _unitOfWork.Save();
                
            }
            catch(Exception e)
            {

            }
            return request;
        }

        public async Task DeclineFriendRequest(int requestId, int userId)
        {
            var request = await this._unitOfWork.FriendRequest.GetFriendRequestById(requestId, userId);
            if (request == null)
            {
                throw new Exception("No such friend request");
            }
            this._unitOfWork.FriendRequest.DeclineFriendRequest(request);
            //_unitOfWork.FriendRequest.Delete(request);
            await _unitOfWork.Save();
        }

        public async Task<List<FriendRequest>> GetAllFriendRequestsForUser(int UserId)
        {
            List<FriendRequest> friends = await this._unitOfWork.FriendRequest.GetFriendRequestsByUser(UserId);
            return friends;
        }
        public async Task<bool> CheckIfFriendRequestSent(int UserId, int FriendId)
        {
            List<FriendRequest> userReqs = await this._unitOfWork.FriendRequest.GetFriendRequestsByUser(UserId);
            foreach(FriendRequest userReq in userReqs)
            {
                if (userReq.RecipientId == FriendId)
                    return true;
            }
            List<FriendRequest> friendReqs = await this._unitOfWork.FriendRequest.GetFriendRequestsByUser(FriendId);
            foreach (FriendRequest friendReq in friendReqs)
            {
                if (friendReq.RecipientId == UserId)
                    return true;
            }

            //var friendsList = await this._unitOfWork.FriendRequest.GetFriendRequestBySenderAndRecipient(UserId, FriendId);
            return false;
        }
    }
}
