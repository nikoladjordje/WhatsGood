using System.ComponentModel.DataAnnotations;
using WhatsGoodApi.Models;

namespace WhatsGoodApi.DTOs
{
    public class MessageDTO
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }

        public MessageDTO(int SenderId, int RecipientId, string Content, DateTime Timestamp)
        {
            this.SenderId = SenderId;
            this.RecipientId = RecipientId;
            this.Content = Content;
            this.Timestamp = Timestamp;
        }
    }
}
