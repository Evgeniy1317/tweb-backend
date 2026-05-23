using Microsoft.EntityFrameworkCore;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.DataAccess;
using SmashHub.Domain;
using SmashHub.Domain.Models.Stringing;

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

        public StringingOrder? Create(StringingOrderCreateModel model, int userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;

            var order = new StringingOrder
            {
                RacketModel = model.RacketModel,
                Tension = model.Tension,
                StringType = model.StringType,
                TotalLei = model.TotalLei,
                ClientUserId = user.Id,
                ClientName = user.Name,
                Status = "handover",
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

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
