using Microsoft.EntityFrameworkCore;
using WhatsGoodApi.Models;
using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private WhatsGoodDbContext _db;
        public MessageRepository(WhatsGoodDbContext db) : base(db)
        {
            _db = db;

        }
        public async Task<List<Message>> GetMessagesBySenderAndRecipient(int SenderId, int RecipientId)
        {
            List<Message> messages = await _db.Messages.Where(x => x.SenderId == SenderId && x.RecipientId == RecipientId).ToListAsync();
            return messages;
        }
    }
}
