using Microsoft.EntityFrameworkCore;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.BusinessLogic.Security;
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
            var user = _db.Users
                .Include(u => u.Contacts)
                .FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                return new UserModel();
            }

            var passwordMatches = PasswordHasher.Verify(model.Password, user.PasswordHash);
            if (!passwordMatches && user.PasswordHash == model.Password)
            {
                user.PasswordHash = PasswordHasher.Hash(model.Password);
                _db.SaveChanges();
                passwordMatches = true;
            }

            if (!passwordMatches) return new UserModel();

            return ToUserModel(user);
        }

        public UserModel UserRegister(UserRegisterModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = PasswordHasher.Hash(model.Password),
                Role = UserRole.User
            };
            _db.Users.Add(user);
            _db.SaveChanges();

            return ToUserModel(user);
        }

        public UserModel? GetProfile(int userId)
        {
            var user = _db.Users
                .Include(u => u.Contacts)
                .FirstOrDefault(u => u.Id == userId);

            return user == null ? null : ToUserModel(user);
        }

        public UserModel? UpdateProfile(int userId, UserProfileUpdateModel model)
        {
            var user = _db.Users
                .Include(u => u.Contacts)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return null;

            if (!string.IsNullOrWhiteSpace(model.Name)) user.Name = model.Name.Trim();
            if (!string.IsNullOrWhiteSpace(model.Email)) user.Email = model.Email.Trim();
            if (model.Phone != null) user.Phone = model.Phone.Trim();

            if (model.Contacts != null)
            {
                _db.UserContacts.RemoveRange(user.Contacts);
                user.Contacts = model.Contacts
                    .Where(c => !string.IsNullOrWhiteSpace(c.Platform) && !string.IsNullOrWhiteSpace(c.Value))
                    .Select(c => new UserContact
                    {
                        Platform = c.Platform.Trim(),
                        Value = c.Value.Trim(),
                        UserId = user.Id
                    })
                    .ToList();
            }

            _db.SaveChanges();
            return ToUserModel(user);
        }

        public List<User> GetAll() => _db.Users.Include(u => u.Contacts).ToList();

        public User? GetById(int id) => _db.Users.Include(u => u.Contacts).FirstOrDefault(u => u.Id == id);

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

        private static UserModel ToUserModel(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role.ToString().ToLowerInvariant(),
                Contacts = user.Contacts.Select(contact => new UserContactModel
                {
                    Id = contact.Id,
                    Platform = contact.Platform,
                    Value = contact.Value
                }).ToList()
            };
        }
    }
}
