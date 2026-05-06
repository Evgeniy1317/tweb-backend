using SmashHub.BusinessLogic.Interfaces;
using SmashHub.DataAccess;
using SmashHub.Domain;
using SmashHub.Domain.Models.User;

namespace SmashHub.BusinessLogic
{
    public class UserBL : IUser
    {
        private readonly SmashHubContext _db;

        public UserBL(SmashHubContext db)
        {
            _db = db;
        }

        public UserModel UserLogin(UserLoginModel model)
        {
            var user = _db.Users.FirstOrDefault(u =>
                u.Email == model.Email && u.Password == model.Password);

            if (user == null) return new UserModel();

            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Avatar = user.Avatar
            };
        }

        public UserModel UserRegister(UserRegisterModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            };
            _db.Users.Add(user);
            _db.SaveChanges();

            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public List<User> GetAll() => _db.Users.ToList();

        public User? GetById(int id) => _db.Users.FirstOrDefault(u => u.Id == id);

        public User? GetByEmail(string email) => _db.Users.FirstOrDefault(u => u.Email == email);

        public bool EmailExists(string email) => _db.Users.Any(u => u.Email == email);

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;
            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }
    }
}