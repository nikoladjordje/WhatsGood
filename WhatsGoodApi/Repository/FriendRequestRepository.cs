using Microsoft.EntityFrameworkCore;
using WhatsGoodApi.Models;
using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Repository
{
    public class FriendRequestRepository : Repository<FriendRequest>, IFriendRequestRepository
    {
        private WhatsGoodDbContext _db;
        public FriendRequestRepository(WhatsGoodDbContext db) : base(db)
        {
            _db = db;

        }
        public async Task<FriendRequest> GetFriendRequestBySenderAndRecipient(int SenderId, int RecipientId)
        {
            var request = await _db.FriendRequests.Where(x => x.SenderId == SenderId && x.RecipientId == RecipientId).FirstOrDefaultAsync();
            return request;
        }
        public async Task<FriendRequest> GetFriendRequestById(int RequestId)
        {
            var request = await _db.FriendRequests.Where(x => x.Id == RequestId).FirstOrDefaultAsync();
            return request;
        }
        public async Task<List<FriendRequest>> GetFriendRequestsByUser(int UserId)
        {
            var requests = await _db.FriendRequests.Where(x => x.RecipientId == UserId && x.IsAccepted == false).ToListAsync();
            return requests;
        }
    }
}
