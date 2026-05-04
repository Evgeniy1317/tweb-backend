using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface ICourt
    {
        List<Court> GetAll();
        Court? GetById(int id);
        Court Create(Court court);
        Court? Update(int id, Court updated);
        bool Delete(int id);
    }
}