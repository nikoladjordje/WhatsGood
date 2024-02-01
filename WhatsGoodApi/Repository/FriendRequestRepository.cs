using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WhatsGoodApi.Models;
using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.WGDbContext;
using Newtonsoft.Json;

namespace WhatsGoodApi.Repository
{
    public class FriendRequestRepository : Repository<FriendRequest>, IFriendRequestRepository
    {
        private WhatsGoodDbContext _db;
        private readonly IConnectionMultiplexer _redis;
        public FriendRequestRepository(WhatsGoodDbContext db, IConnectionMultiplexer redis) : base(db)
        {
            _db = db;
            _redis = redis;
        }
        public async Task<FriendRequest> SendFriendRequest(FriendRequest friendRequest)
        {
            this._db.FriendRequests.Add(friendRequest);
            _db.SaveChanges();

            FriendRequest fr = friendRequest;

            var redis = _redis.GetDatabase();

            var key = $"friendRequests:{friendRequest.RecipientId}";
            await redis.ListRightPushAsync(key, JsonConvert.SerializeObject(fr));

            return friendRequest;
        }

        public async Task<FriendRequest> AcceptFriendRequest(FriendRequest request)
        {

            var redis = _redis.GetDatabase();

            var key = $"friendRequests:{request.RecipientId}";
            var value = JsonConvert.SerializeObject(request);
            await redis.ListRemoveAsync(key, value);

            request.IsAccepted = true;
            this._db.FriendRequests.Update(request);

            await redis.ListRightPushAsync(key, JsonConvert.SerializeObject(request));

            return request;
        }

        public async Task<FriendRequest> DeclineFriendRequest(FriendRequest request)
        {

            var redis = _redis.GetDatabase();

            var key = $"friendRequests:{request.RecipientId}";
            var value = JsonConvert.SerializeObject(request);
            await redis.ListRemoveAsync(key, value);

            this._db.FriendRequests.Remove(request);

            return request;
        }

        public async Task<FriendRequest> GetFriendRequestBySenderAndRecipient(int SenderId, int RecipientId)
        {
            var request = await _db.FriendRequests.Where(x => x.SenderId == SenderId && x.RecipientId == RecipientId).FirstOrDefaultAsync();
            return request;
        }
        public async Task<FriendRequest> GetFriendRequestById(int RequestId, int UserId)
        {
            var redis = _redis.GetDatabase();

            var key = $"friendRequests:{UserId}";
            var cachedRequests = await redis.ListRangeAsync(key);

            if (cachedRequests.Any())
            {
                var redisRequests = cachedRequests.Select(item => JsonConvert.DeserializeObject<FriendRequest>(item)).ToList();
                foreach (FriendRequest req in redisRequests)
                {
                    if (req.Id == RequestId)
                        return req;
                }
            }

            var request = await _db.FriendRequests.Where(x => x.Id == RequestId).FirstOrDefaultAsync();
            if (request != null)
                await redis.ListRightPushAsync(key, JsonConvert.SerializeObject(request));

            return request;
        }
        public async Task<List<FriendRequest>> GetFriendRequestsByUser(int UserId)
        {
            var redis = _redis.GetDatabase();

            var key = $"friendRequests:{UserId}";
            var cachedRequests = await redis.ListRangeAsync(key);

            if (cachedRequests.Any())
            {
                var redisRequests = cachedRequests.Select(item => JsonConvert.DeserializeObject<FriendRequest>(item)).ToList();
                return redisRequests;
            }

            var requests = await _db.FriendRequests.Where(x => x.RecipientId == UserId && x.IsAccepted == false).ToListAsync();

            foreach(FriendRequest req in requests)
            {
                req.Sender = null;
                req.Recipient = null;
            }

            if(requests != null)
            {
                var serializedRequests = requests.Select(item => JsonConvert.SerializeObject(item)).ToArray();
                await redis.ListRightPushAsync(key, serializedRequests.Select(item => (RedisValue)item).ToArray());
            }

            return requests;
        }

    }
}
