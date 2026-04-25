using SmashHub.BusinessLogic.Core;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;
using SmashHub.Helpers;

namespace SmashHub.BusinessLogic
{
    public class UserBL : UserApi, IUser
    {
        private static List<User> _users = new();

        public override UserModel UserLogin(UserLoginModel model)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email == model.Email && u.Password == model.Password);
            if (user == null) return new UserModel();
            return new UserModel { Id = user.Id, Name = user.Name, Email = user.Email, Phone = user.Phone, Avatar = user.Avatar };
        }

        public override UserModel UserRegister(UserRegisterModel model)
        {
            var user = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            };
            _users.Add(user);
            return new UserModel { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public override List<User> GetAll() => _users;
        public override User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);
        public override User? GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);
        public override bool EmailExists(string email) => _users.Any(u => u.Email == email);

        public override bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;
            _users.Remove(user);
            return true;
        }
    }
}