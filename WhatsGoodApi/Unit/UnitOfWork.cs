using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.Repository;
using WhatsGoodApi.WGDbContext;
using StackExchange.Redis;

namespace WhatsGoodApi.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WhatsGoodDbContext _context;
        public IMessageRepository Message { get; private set; }
        public IUserRepository User { get; private set; }
        public IFriendshipRepository Friendship { get; private set; }
        public IFriendRequestRepository FriendRequest { get; set; }
        private readonly IConnectionMultiplexer _redis;
        public UnitOfWork (WhatsGoodDbContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
            Message = new MessageRepository(_context, _redis);
            User = new UserRepository(_context,_redis);
            Friendship = new FriendshipRepository(_context, _redis);
            FriendRequest = new FriendRequestRepository(_context, _redis);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
