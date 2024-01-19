using System.ComponentModel.DataAnnotations;

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
        public virtual User? Sender { get; set; }
        [Required]
        public int RecipientId { get; set; }
        public virtual User? Recipient { get; set; }
    }
}
