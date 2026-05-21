using SmashHub.BusinessLogic.Interfaces;
using SmashHub.DataAccess;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class ProductBL : IProduct
    {
        private readonly SmashHubContext _db;

        public ProductBL(SmashHubContext db)
        {
            _db = db;
        }

        public List<Product> GetAll() => _db.Products.ToList();

        public Product? GetById(int id) => _db.Products.FirstOrDefault(p => p.Id == id);

        public Product Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product? Update(int id, Product updated)
        {
            var product = GetById(id);
            if (product == null) return null;

            product.Title = updated.Title;
            product.Price = updated.Price;
            product.Description = updated.Description;
            product.Category = updated.Category;
            product.Condition = updated.Condition;
            product.Image = updated.Image;
            product.SizeLabel = updated.SizeLabel;
            product.ColorLabel = updated.ColorLabel;
            product.Fit = updated.Fit;
            product.SellerPhone = updated.SellerPhone;

            _db.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return false;
            _db.Products.Remove(product);
            _db.SaveChanges();
            return true;
        }
    }
}