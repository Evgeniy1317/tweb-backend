namespace SmashHub.Domain.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string? SizeLabel { get; set; }
        public string? ColorLabel { get; set; }
        public string? Fit { get; set; }
        public string? SellerPhone { get; set; }
        public int? OwnerId { get; set; }
        public List<string> ExtraImages { get; set; } = new();
        public List<SellerContactSnapshotModel> SellerContacts { get; set; } = new();
    }
}
