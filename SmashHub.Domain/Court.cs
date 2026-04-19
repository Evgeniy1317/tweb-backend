using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain
{
    public class Court
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Hours { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
        public string CoachPhone { get; set; } = string.Empty;
        public int Courts { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}