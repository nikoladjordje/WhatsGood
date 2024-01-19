using System.ComponentModel.DataAnnotations;

namespace WhatsGoodApi.Models
{
    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; }
        public virtual User? Sender { get; set; }
        public int RecipientId { get; set; }
        public virtual User? Recipient { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime Timestamp { get; set; }
        public FriendRequest(int senderId, int recipientId, bool isAccepted, DateTime timestamp)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            IsAccepted = isAccepted;
            Timestamp = timestamp;
        }
    }
}
