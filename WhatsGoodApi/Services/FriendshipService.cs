using WhatsGoodApi.Models;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.Unit;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly WhatsGoodDbContext _db;
        public UnitOfWork _unitOfWork { get; set; }

        public FriendshipService(WhatsGoodDbContext db)
        {
            this._db = db;
            this._unitOfWork = new UnitOfWork(db);
        }
        public async Task CreateFriendship(int requestId)
        {
            var request = await this._unitOfWork.FriendRequest.GetFriendRequestById(requestId);
            if (request != null)
            {
                var friendsList = await this._unitOfWork.Friendship.GetFriendshipByUserAndFriend(request.SenderId, request.RecipientId);
                if (friendsList == null)
                {
                    var friendslistCreated1 = new Friendship(request.SenderId, request.RecipientId);
                    var friendslistCreated2 = new Friendship(request.RecipientId, request.SenderId);
                    await _unitOfWork.Friendship.Add(friendslistCreated1);
                    await _unitOfWork.Friendship.Add(friendslistCreated2);
                    await _unitOfWork.Save();
                }

            }
        }

        public async Task<List<Friendship>> GetAllFriendsForUser(int UserId)
        {
            List<Friendship> friends = await this._unitOfWork.Friendship.GetFriendshipsByUser(UserId);
            return friends;
        }

        public async Task<bool> CheckIfFriends(string UserName, string FriendName)
        {
            var friend1 = await this._unitOfWork.User.GetUserByUsername(UserName);
            var friend2 = await this._unitOfWork.User.GetUserByUsername(FriendName);
            if (friend1 == null || friend2 == null)
            {
                return false;
            }
            var friendsList = await this._unitOfWork.Friendship.GetFriendshipByUserAndFriend(friend1.ID, friend2.ID);
            if (friendsList == null)
                return false;
            return true;
        }
    }
}
