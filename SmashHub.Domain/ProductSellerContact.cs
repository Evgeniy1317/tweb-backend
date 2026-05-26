using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmashHub.Domain
{
    public class ProductSellerContact
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(30)]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Value { get; set; } = string.Empty;

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
