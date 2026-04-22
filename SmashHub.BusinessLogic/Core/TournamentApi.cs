using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Core
{
    public abstract class TournamentApi
    {
        public abstract List<Tournament> GetAll();
    }
}