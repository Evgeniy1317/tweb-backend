using Microsoft.EntityFrameworkCore;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.DataAccess;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class StringingOrderBL : IStringingOrder
    {
        private readonly SmashHubContext _db;

        public StringingOrderBL(SmashHubContext db)
        {
            _db = db;
        }

        public List<StringingOrder> GetAll() => _db.StringingOrders.Include(o => o.Client).ToList();

        public StringingOrder Create(StringingOrder order)
        {
            order.Status = "handover";
            order.CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd");
            _db.StringingOrders.Add(order);
            _db.SaveChanges();
            return order;
        }

        public StringingOrder? UpdateStatus(int id, string status)
        {
            var order = _db.StringingOrders.FirstOrDefault(o => o.Id == id);
            if (order == null) return null;

            order.Status = status;
            _db.SaveChanges();
            return order;
        }
    }
}