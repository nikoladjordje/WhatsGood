using WhatsGoodApi.DTOs;
using WhatsGoodApi.Models;

namespace WhatsGoodApi.Services.IServices
{
    public interface IMessageService
    {
        Task SendMessage(MessageDTO message);
        Task<List<Message>> GetAllMessagesForChat(int SenderId, int RecipientId);
    }
}
