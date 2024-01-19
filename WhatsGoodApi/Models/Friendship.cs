using System.ComponentModel.DataAnnotations;

namespace WhatsGoodApi.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int FriendId { get; set; }
        public virtual User? Friend { get; set; }
        public Friendship(int userId, int friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }
    }
}
