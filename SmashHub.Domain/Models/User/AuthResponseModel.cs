namespace SmashHub.Domain.Models.User
{
    public class AuthResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public UserModel User { get; set; } = new();
    }
}
