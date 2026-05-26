using SmashHub.Domain.Models.Product;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        List<ProductModel> GetAll();
        ProductModel? GetById(int id);
        ProductModel Create(ProductCreateModel model, int ownerId);
        ProductModel? Update(int id, ProductUpdateModel model);
        bool Delete(int id);
    }
}
