namespace SmashHub.Domain.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<UserContactModel> Contacts { get; set; } = new();
        public List<int> Favorites { get; set; } = new();
        public string Role { get; set; } = "user";
    }
}
