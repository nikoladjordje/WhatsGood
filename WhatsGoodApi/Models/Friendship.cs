using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
//using System.Text.Json.Serialization;

namespace WhatsGoodApi.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
        public int FriendId { get; set; }
        [JsonIgnore]
        public virtual User? Friend { get; set; }
        public Friendship(int userId, int friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }
    }
}
