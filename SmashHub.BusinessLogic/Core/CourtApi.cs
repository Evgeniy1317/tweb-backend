using SmashHub.Domain;

namespace SmashHub.BusinessLogic.Core
{
    public abstract class CourtApi
    {
        public abstract List<Court> GetAll();
    }
}