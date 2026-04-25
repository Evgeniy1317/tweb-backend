using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface IStringingOrder
    {
        List<StringingOrder> GetAll();
        StringingOrder Create(StringingOrder order);
        StringingOrder? UpdateStatus(int id, string status);
    }
}