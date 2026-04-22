using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Core
{
    public abstract class ProductApi
    {
        public abstract List<Product> GetAll();
        public abstract Product? GetById(int id);
        public abstract Product Create(Product product);
        public abstract Product? Update(int id, Product updated);
        public abstract bool Delete(int id);
    }
}
