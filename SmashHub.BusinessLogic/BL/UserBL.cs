using SmashHub.Domain;
using SmashHub.Helpers;
using SmashHub.Model;
using System.Collections.Generic;

namespace SmashHub.Data
{
    public class UserBL : IUser
    {
        private static List<User> _users = new();

        public List<User> GetAll() => _users;

        public User? GetById(int id) =>
            _users.FirstOrDefault(u => u.Id == id);

        public User? GetByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email == email);

        public bool EmailExists(string email) =>
            _users.Any(u => u.Email == email);

        public UserModel Create(UserRegisterModel model)
        {
            var user = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            };
            _users.Add(user);
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public User? Update(int id, User updated)
        {
            var user = GetById(id);
            if (user == null) return null;
            user.Name = updated.Name;
            user.Email = updated.Email;
            user.Phone = updated.Phone;
            user.Avatar = updated.Avatar;
            return user;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;
            _users.Remove(user);
            return true;
        }
    }
}