using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmashHub.Domain
{
    public enum UserRole
    {
        User = 1,
        Manager = 20,
        Admin = 30
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.User;

        public string Phone { get; set; } = string.Empty;

        public List<UserContact> Contacts { get; set; } = new();

        [JsonIgnore]
        public List<StringingOrder> StringingOrders { get; set; } = new();

        [JsonIgnore]
        public List<Product> Products { get; set; } = new();
    }
}
