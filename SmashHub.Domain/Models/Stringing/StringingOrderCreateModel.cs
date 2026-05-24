using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain.Models.Stringing
{
    public class StringingOrderCreateModel
    {
        [Required]
        public string RacketModel { get; set; } = string.Empty;

        [Required]
        public string Tension { get; set; } = string.Empty;

        [Required]
        public string StringType { get; set; } = string.Empty;

        [Range(0, 999999)]
        public decimal TotalLei { get; set; }
    }
}
