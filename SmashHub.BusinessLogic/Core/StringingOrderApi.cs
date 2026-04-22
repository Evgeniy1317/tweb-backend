using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Core
{
    public abstract class StringingOrderApi
    {
        public abstract List<StringingOrder> GetAll();
        public abstract StringingOrder Create(StringingOrder order);
        public abstract StringingOrder? UpdateStatus(int id, string status);
    }
}
