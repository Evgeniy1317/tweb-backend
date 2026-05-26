using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain.Models.Product
{
    public class SellerContactSnapshotModel
    {
        [Required]
        public string Platform { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
