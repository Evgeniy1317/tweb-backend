using SmashHub.Domain;
using SmashHub.Helpers;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserModel UserLogin(UserLoginModel model);
        UserModel UserRegister(UserRegisterModel model);
        User? GetById(int id);
        User? GetByEmail(string email);
        bool EmailExists(string email);
        List<User> GetAll();
        bool Delete(int id);
    }
}