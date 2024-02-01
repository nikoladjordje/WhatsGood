using WhatsGoodApi.Models;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.Unit;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly WhatsGoodDbContext _db;
        public IUnitOfWork _unitOfWork { get; set; }

        public FriendshipService(WhatsGoodDbContext db, IUnitOfWork unitOfWork)
        {
            this._db = db;
            this._unitOfWork = unitOfWork;
        }
        public async Task CreateFriendship(FriendRequest request, int userId)
        {
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

        public async Task<List<User>> GetAllFriendsForUser(int UserId)
        {
            List<User> friends = await this._unitOfWork.Friendship.GetFriendsForUser(UserId);
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
