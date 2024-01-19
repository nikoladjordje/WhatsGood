using WhatsGoodApi.DTOs;
using WhatsGoodApi.Models;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.Unit;
using WhatsGoodApi.WGDbContext;


namespace WhatsGoodApi.Services
{
    public class MessageService : IMessageService
    {
        private readonly WhatsGoodDbContext _db;
        public UnitOfWork _unitOfWork { get; set; }

        public MessageService(WhatsGoodDbContext db)
        {
            this._db = db;
            this._unitOfWork = new UnitOfWork(db);
        }

        public async Task SendMessage(MessageDTO message)
        {
            if (message != null)
            {
                var messageCreated = new Message(message.SenderId, message.RecipientId, message.Content, message.Timestamp);
                await _unitOfWork.Message.Add(messageCreated);
                await _unitOfWork.Save();
            }
        }
        public async Task<List<Message>> GetAllMessagesForChat(int SenderId, int RecipientId)
        {
            List<Message> messages = await this._unitOfWork.Message.GetMessagesBySenderAndRecipient(SenderId, RecipientId);
            return messages;
        }
    }
}
