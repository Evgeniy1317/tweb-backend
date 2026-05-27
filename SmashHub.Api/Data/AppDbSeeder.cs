using Microsoft.EntityFrameworkCore;
using SmashHub.BusinessLogic.Security;
using SmashHub.DataAccess;
using SmashHub.Domain;

namespace SmashHub.Api.Data
{
    public static class AppDbSeeder
    {
        public static void Seed(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SmashHubContext>();

            db.Database.Migrate();
            SeedUsers(db);
        }

        private static void SeedUsers(SmashHubContext db)
        {
            UpsertUser(
                db,
                name: "Eugeniy",
                email: "eugeniy@smashhub.local",
                password: "Admin123!",
                role: UserRole.Admin,
                phone: "+37360000001");

            UpsertUser(
                db,
                name: "Anzor",
                email: "anzor@smashhub.local",
                password: "Manager123!",
                role: UserRole.Manager,
                phone: "+37360000002");

            UpsertUser(
                db,
                name: "User",
                email: "user@smashhub.local",
                password: "User123!",
                role: UserRole.User,
                phone: "+37360000003");

            db.SaveChanges();
        }

        private static void UpsertUser(
            SmashHubContext db,
            string name,
            string email,
            string password,
            UserRole role,
            string phone)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                db.Users.Add(new User
                {
                    Name = name,
                    Email = email,
                    PasswordHash = PasswordHasher.Hash(password),
                    Role = role,
                    Phone = phone
                });
                return;
            }

            user.Name = name;
            user.Role = role;
            user.Phone = phone;
            if (!PasswordHasher.Verify(password, user.PasswordHash))
            {
                user.PasswordHash = PasswordHasher.Hash(password);
            }
        }
    }
}
