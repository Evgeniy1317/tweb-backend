using Microsoft.EntityFrameworkCore;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.DataAccess;
using SmashHub.Domain;
using SmashHub.Domain.Models.Cart;

namespace SmashHub.BusinessLogic
{
    public class CartBL : ICart
    {
        private readonly SmashHubContext _db;

        public CartBL(SmashHubContext db)
        {
            _db = db;
        }

        public List<CartItemModel> GetByUserId(int userId)
        {
            return _db.CartItems
                .Include(item => item.Product)
                .Where(item => item.UserId == userId)
                .OrderByDescending(item => item.CreatedAt)
                .AsEnumerable()
                .Where(item => item.Product != null)
                .Select(ToCartItemModel)
                .ToList();
        }

        public CartItemModel? Add(int userId, int productId)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return null;

            var existing = _db.CartItems
                .Include(item => item.Product)
                .FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);

            if (existing != null)
            {
                return ToCartItemModel(existing);
            }

            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Product = product,
                CreatedAt = DateTime.UtcNow
            };

            _db.CartItems.Add(cartItem);
            _db.SaveChanges();
            return ToCartItemModel(cartItem);
        }

        public bool Remove(int userId, int productId)
        {
            var cartItem = _db.CartItems
                .FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);

            if (cartItem == null) return false;

            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();
            return true;
        }

        public void Clear(int userId)
        {
            var cartItems = _db.CartItems.Where(item => item.UserId == userId).ToList();
            if (cartItems.Count == 0) return;

            _db.CartItems.RemoveRange(cartItems);
            _db.SaveChanges();
        }

        private static CartItemModel ToCartItemModel(CartItem item)
        {
            return new CartItemModel
            {
                Id = item.ProductId,
                Title = item.Product?.Title ?? string.Empty,
                Price = item.Product?.Price ?? 0
            };
        }
    }
}
