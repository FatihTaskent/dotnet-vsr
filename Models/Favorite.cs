namespace dotnet_core.Models
{
    public class Favorite
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}