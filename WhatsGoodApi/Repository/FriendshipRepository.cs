using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using WhatsGoodApi.Models;
using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Repository
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        private WhatsGoodDbContext _db;
        private readonly IConnectionMultiplexer _redis;
        public FriendshipRepository(WhatsGoodDbContext db, IConnectionMultiplexer redis) : base(db)
        {
            _db = db;
            _redis = redis;
        }
        public async Task<List<User>> GetFriendsForUser(int UserId)
        {
            var redis = _redis.GetDatabase();

            var key = $"friends:{UserId}";
            var cachedFriends = await redis.ListRangeAsync(key);

            if (cachedFriends.Any())
            {
                var redisFriends = cachedFriends.Select(item => JsonConvert.DeserializeObject<User>(item)).ToList();
                return redisFriends;
            }

            List<Friendship> friendships = await _db.Friendships.Where(x => x.UserId == UserId).ToListAsync();
            List<User> friends = new List<User>();

            foreach (Friendship friend in friendships)
            {
                User a = await this._db.Users.Where(x => x.ID == friend.FriendId).FirstOrDefaultAsync();
                a.FriendFriendships = null;
                a.InitiatorFriendships = null;
                friends.Add(a);
            }

            //if (friends != null)
            //{
            //    var serializedFriends = friends.Select(item => JsonConvert.SerializeObject(item)).ToArray();
            //    await redis.ListRightPushAsync(key, serializedFriends.Select(item => (RedisValue)item).ToArray());
            //}

            return friends;
        }
        public async Task<Friendship> GetFriendshipByUserAndFriend(int UserId, int FriendId)
        {
            var friendship = await _db.Friendships.Where(x => x.UserId == UserId && x.FriendId == FriendId).FirstOrDefaultAsync();
            return friendship;
        }
        public async Task<Friendship> CreateFriendship(Friendship friendship)
        {
            _db.Friendships.Add(friendship);
            await _db.SaveChangesAsync();
            var redis = _redis.GetDatabase();

            var key = $"friends:{friendship.UserId}";

            User a = await this._db.Users.Where(x => x.ID == friendship.FriendId).FirstOrDefaultAsync();

            await redis.ListRightPushAsync(key, JsonConvert.SerializeObject(a));

            return friendship;
        }
    }
}
