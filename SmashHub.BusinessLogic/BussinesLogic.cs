using SmashHub.BusinessLogic.Interfaces;

namespace SmashHub.BusinessLogic
{
    public class BussinesLogic
    {
        public IUser GetUserBL() => new UserBL();
        public IProduct GetProductBL() => new ProductBL();
        public IStringingOrder GetStringingOrderBL() => new StringingOrderBL();
        public ICourt GetCourtBL() => new CourtBL();
        public ITournament GetTournamentBL() => new TournamentBL();
    }
}