using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class TournamentBL : ITournament
    {
        private readonly SmashHubContext _db;

        public TournamentBL(SmashHubContext db)
        {
            _db = db;
        }

        public List<Tournament> GetAll() => _db.Tournaments.ToList();

        public Tournament? GetById(int id) => _db.Tournaments.FirstOrDefault(t => t.Id == id);

        public Tournament Create(Tournament tournament)
        {
            _db.Tournaments.Add(tournament);
            _db.SaveChanges();
            return tournament;
        }

        public Tournament? Update(int id, Tournament updated)
        {
            var tournament = GetById(id);
            if (tournament == null) return null;

            tournament.Title = updated.Title;
            tournament.Date = updated.Date;
            tournament.Location = updated.Location;
            tournament.Level = updated.Level;
            tournament.Description = updated.Description;
            tournament.ExternalUrl = updated.ExternalUrl;

            _db.SaveChanges();
            return tournament;
        }

        public bool Delete(int id)
        {
            var tournament = GetById(id);
            if (tournament == null) return false;
            _db.Tournaments.Remove(tournament);
            _db.SaveChanges();
            return true;
        }
    }
}