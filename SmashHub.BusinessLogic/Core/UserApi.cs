using SmashHub.Domain;
using SmashHub.Domain.Models.User;

namespace SmashHub.BusinessLogic.Core
{
    public abstract class UserApi
    {
        public abstract UserModel UserLogin(UserLoginModel model);
        public abstract UserModel UserRegister(UserRegisterModel model);
        public abstract User? GetById(int id);
        public abstract User? GetByEmail(string email);
        public abstract bool EmailExists(string email);
        public abstract List<User> GetAll();
        public abstract bool Delete(int id);
    }
}