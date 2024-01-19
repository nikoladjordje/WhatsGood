using Microsoft.EntityFrameworkCore;
using WhatsGoodApi.Models;
using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Repository
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        private WhatsGoodDbContext _db;
        public FriendshipRepository(WhatsGoodDbContext db) : base(db)
        {
            _db = db;

        }
        public async Task<List<Friendship>> GetFriendshipsByUser(int UserId)
        {
            var friends = await _db.Friendships.Include(x => x.Friend).Where(x => x.UserId == UserId).ToListAsync();
            return friends;
        }
        public async Task<Friendship> GetFriendshipByUserAndFriend(int UserId, int FriendId)
        {
            var friendship = await _db.Friendships.Where(x => x.UserId == UserId && x.FriendId == FriendId).FirstOrDefaultAsync();
            return friendship;
        }
    }
}
