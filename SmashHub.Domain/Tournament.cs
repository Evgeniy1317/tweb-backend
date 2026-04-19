using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ExternalUrl { get; set; } = string.Empty;
    }
}