using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.Repository;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WhatsGoodDbContext _context;
        public IMessageRepository Message { get; private set; }
        public IUserRepository User { get; private set; }
        public IFriendshipRepository Friendship { get; private set; }
        public IFriendRequestRepository FriendRequest { get; set; }
        public UnitOfWork (WhatsGoodDbContext context)
        {
            _context = context;
            Message = new MessageRepository(_context);
            User = new UserRepository(_context);
            Friendship = new FriendshipRepository(_context);
            FriendRequest = new FriendRequestRepository(_context);
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
