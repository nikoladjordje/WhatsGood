namespace WhatsGoodApi.DTOs
{
    public class FriendRequestDTO
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
