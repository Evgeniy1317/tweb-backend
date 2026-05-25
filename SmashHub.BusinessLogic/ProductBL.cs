using SmashHub.BusinessLogic.Interfaces;
using SmashHub.DataAccess;
using SmashHub.Domain;
using SmashHub.Domain.Models.Product;

namespace SmashHub.BusinessLogic
{
    public class ProductBL : IProduct
    {
        private readonly SmashHubContext _db;

        public ProductBL(SmashHubContext db)
        {
            _db = db;
        }

        public List<ProductModel> GetAll()
        {
            return _db.Products
                .Select(product => ToProductModel(product))
                .ToList();
        }

        public ProductModel? GetById(int id)
        {
            var product = GetEntityById(id);
            return product == null ? null : ToProductModel(product);
        }

        public ProductModel Create(ProductCreateModel model)
        {
            var product = new Product
            {
                Title = model.Title,
                Price = model.Price,
                Description = model.Description,
                Category = model.Category,
                Condition = model.Condition,
                Image = model.Image,
                SizeLabel = model.SizeLabel,
                ColorLabel = model.ColorLabel,
                Fit = model.Fit,
                SellerPhone = model.SellerPhone
            };

            _db.Products.Add(product);
            _db.SaveChanges();
            return ToProductModel(product);
        }

        public ProductModel? Update(int id, ProductUpdateModel model)
        {
            var product = GetEntityById(id);
            if (product == null) return null;

            product.Title = model.Title;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Category = model.Category;
            product.Condition = model.Condition;
            product.Image = model.Image;
            product.SizeLabel = model.SizeLabel;
            product.ColorLabel = model.ColorLabel;
            product.Fit = model.Fit;
            product.SellerPhone = model.SellerPhone;

            _db.SaveChanges();
            return ToProductModel(product);
        }

        public bool Delete(int id)
        {
            var product = GetEntityById(id);
            if (product == null) return false;
            _db.Products.Remove(product);
            _db.SaveChanges();
            return true;
        }

        private Product? GetEntityById(int id) => _db.Products.FirstOrDefault(p => p.Id == id);

        private static ProductModel ToProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Condition = product.Condition,
                Image = product.Image,
                SizeLabel = product.SizeLabel,
                ColorLabel = product.ColorLabel,
                Fit = product.Fit,
                SellerPhone = product.SellerPhone
            };
        }
    }
}
