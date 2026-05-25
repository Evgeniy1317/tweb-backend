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

        public List<StringingOrderModel> GetAll() => _db.StringingOrders.Select(order => new StringingOrderModel
        {
            Id = order.Id,
            RacketModel = order.RacketModel,
            Tension = order.Tension,
            StringType = order.StringType,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            ClientUserId = order.ClientUserId,
            ClientName = order.ClientName,
            TotalLei = order.TotalLei
        }).ToList();

        public List<StringingOrderModel> GetByUserId(int userId)
        {
            return _db.StringingOrders
                .Where(o => o.ClientUserId == userId)
                .Select(order => new StringingOrderModel
                {
                    Id = order.Id,
                    RacketModel = order.RacketModel,
                    Tension = order.Tension,
                    StringType = order.StringType,
                    Status = order.Status,
                    CreatedAt = order.CreatedAt,
                    ClientUserId = order.ClientUserId,
                    ClientName = order.ClientName,
                    TotalLei = order.TotalLei
                })
                .ToList();
        }

        public StringingOrderModel? Create(StringingOrderCreateModel model, int userId)
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
                CreatedAt = DateTime.UtcNow
            };

            _db.StringingOrders.Add(order);
            _db.SaveChanges();
            return ToStringingOrderModel(order);
        }

        public StringingOrderModel? UpdateStatus(int id, string status)
        {
            var order = _db.StringingOrders.FirstOrDefault(o => o.Id == id);
            if (order == null) return null;

            order.Status = status;
            _db.SaveChanges();
            return ToStringingOrderModel(order);
        }

        private static StringingOrderModel ToStringingOrderModel(StringingOrder order)
        {
            return new StringingOrderModel
            {
                Id = order.Id,
                RacketModel = order.RacketModel,
                Tension = order.Tension,
                StringType = order.StringType,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                ClientUserId = order.ClientUserId,
                ClientName = order.ClientName,
                TotalLei = order.TotalLei
            };
        }
    }
}
