using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhatsGoodApi.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Friendship> InitiatorFriendships { get; set; }
        [JsonIgnore]
        public virtual ICollection<Friendship> FriendFriendships { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> SenderLists { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> RecipientLists { get; set; }
    }
}
