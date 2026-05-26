using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmashHub.Domain
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string Url { get; set; } = string.Empty;

        public int SortOrder { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
