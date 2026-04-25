using SmashHub.BusinessLogic.Core;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class ProductBL : ProductApi, IProduct
    {
        private static List<Product> _products = new();

        public override List<Product> GetAll() => _products;
        public override Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public override Product Create(Product product)
        {
            product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return product;
        }

        public override Product? Update(int id, Product updated)
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
            return product;
        }

        public override bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return false;
            _products.Remove(product);
            return true;
        }
    }
}