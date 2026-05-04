using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface ITournament
    {
        List<Tournament> GetAll();
        Tournament? GetById(int id);
        Tournament Create(Tournament tournament);
        Tournament? Update(int id, Tournament updated);
        bool Delete(int id);
    }
}