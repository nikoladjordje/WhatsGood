using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
//using System.Text.Json.Serialization;
namespace WhatsGoodApi.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        [Required]
        public int SenderId { get; set; }
        [JsonIgnore]
        public virtual User? Sender { get; set; }
        [Required]
        public int RecipientId { get; set; }
        [JsonIgnore]
        public virtual User? Recipient { get; set; }
        public Message(int senderId, int recipientId, string content, DateTime timestamp)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            Content = content;
            Timestamp = timestamp;
        }
    }
}
