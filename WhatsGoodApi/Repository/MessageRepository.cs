using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WhatsGoodApi.Models;
using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private WhatsGoodDbContext _db;
        private readonly IConnectionMultiplexer _redis;
        public MessageRepository(WhatsGoodDbContext db, IConnectionMultiplexer redis) : base(db)
        {
            _db = db;
            _redis = redis;      
        }
        public async Task<List<Message>> GetMessagesBySenderAndRecipient(int SenderId, int RecipientId)
        {
            List<Message> messages = await _db.Messages.Where(x => x.SenderId == SenderId && x.RecipientId == RecipientId).ToListAsync();
            return messages;
        }
    }
}
