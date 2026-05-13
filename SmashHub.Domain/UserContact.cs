using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain
{
    public class UserContact
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Value { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
