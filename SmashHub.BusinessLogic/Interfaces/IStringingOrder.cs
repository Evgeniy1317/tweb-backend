using SmashHub.Domain.Models.Stringing;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface IStringingOrder
    {
        List<StringingOrderModel> GetAll();
        List<StringingOrderModel> GetByUserId(int userId);
        StringingOrderModel? Create(StringingOrderCreateModel model, int userId);
        StringingOrderModel? UpdateStatus(int id, string status);
    }
}
