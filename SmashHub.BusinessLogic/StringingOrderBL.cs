using SmashHub.BusinessLogic.Core;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class StringingOrderBL : StringingOrderApi, IStringingOrder
    {
        private static List<StringingOrder> _orders = new();

        public override List<StringingOrder> GetAll() => _orders;

        public override StringingOrder Create(StringingOrder order)
        {
            order.Id = _orders.Count > 0 ? _orders.Max(o => o.Id) + 1 : 1;
            order.Status = "handover";
            order.CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd");
            _orders.Add(order);
            return order;
        }

        public override StringingOrder? UpdateStatus(int id, string status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return null;
            order.Status = status;
            return order;
        }
    }
}