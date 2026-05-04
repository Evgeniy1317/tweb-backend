using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class CourtBL : ICourt
    {
        private readonly SmashHubContext _db;

        public CourtBL(SmashHubContext db)
        {
            _db = db;
        }

        public List<Court> GetAll() => _db.Courts.ToList();

        public Court? GetById(int id) => _db.Courts.FirstOrDefault(c => c.Id == id);

        public Court Create(Court court)
        {
            _db.Courts.Add(court);
            _db.SaveChanges();
            return court;
        }

        public Court? Update(int id, Court updated)
        {
            var court = GetById(id);
            if (court == null) return null;

            court.Name = updated.Name;
            court.Address = updated.Address;
            court.Phone = updated.Phone;
            court.Hours = updated.Hours;
            court.Coach = updated.Coach;
            court.CoachPhone = updated.CoachPhone;
            court.Courts = updated.Courts;
            court.Image = updated.Image;

            _db.SaveChanges();
            return court;
        }

        public bool Delete(int id)
        {
            var court = GetById(id);
            if (court == null) return false;
            _db.Courts.Remove(court);
            _db.SaveChanges();
            return true;
        }
    }
}