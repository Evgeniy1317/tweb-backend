namespace SmashHub.Domain.Models.User
{
    public class UserContactModel
    {
        public int Id { get; set; }
        public string Platform { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
