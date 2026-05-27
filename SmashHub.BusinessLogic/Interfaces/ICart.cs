using SmashHub.Domain.Models.Cart;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface ICart
    {
        List<CartItemModel> GetByUserId(int userId);
        CartItemModel? Add(int userId, int productId);
        bool Remove(int userId, int productId);
        void Clear(int userId);
    }
}
