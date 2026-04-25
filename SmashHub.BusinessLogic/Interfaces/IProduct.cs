using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAll();
        Product? GetById(int id);
        Product Create(Product product);
        Product? Update(int id, Product updated);
        bool Delete(int id);
    }
}