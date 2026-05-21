using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain.Models.User
{
    public class UserProfileUpdateModel
    {
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public List<UserContactModel>? Contacts { get; set; }
    }
}
