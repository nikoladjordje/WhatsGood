using WhatsGoodApi.Models;

namespace WhatsGoodApi.Repository.IRepository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetMessagesBySenderAndRecipient(int SenderId, int RecipientId);
    }
}
