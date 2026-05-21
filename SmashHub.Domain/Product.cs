using System.ComponentModel.DataAnnotations;

namespace SmashHub.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Range(0.01, 999999)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Condition { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        public string? SizeLabel { get; set; }
        public string? ColorLabel { get; set; }
        public string? Fit { get; set; }
        public string? SellerPhone { get; set; }
    }
}