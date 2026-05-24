using SmashHub.Domain;
using SmashHub.Domain.Models.Stringing;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface IStringingOrder
    {
        List<StringingOrder> GetAll();
        List<StringingOrder> GetByUserId(int userId);
        StringingOrder? Create(StringingOrderCreateModel model, int userId);
        StringingOrder? UpdateStatus(int id, string status);
    }
}
