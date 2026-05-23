using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain.Models.Stringing
{
    public class StringingOrderStatusUpdateModel
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
