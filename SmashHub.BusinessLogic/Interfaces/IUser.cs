using SmashHub.Domain;
using SmashHub.Domain.Models.User;

namespace SmashHub.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserModel UserLogin(UserLoginModel model);
        UserModel UserRegister(UserRegisterModel model);
        UserModel? GetProfile(int userId);
        UserModel? UpdateProfile(int userId, UserProfileUpdateModel model);
        User? GetById(int id);
        User? GetByEmail(string email);
        bool EmailExists(string email);
        bool EmailExistsForOtherUser(int userId, string email);
        List<User> GetAll();
        bool Delete(int id);
    }
}
