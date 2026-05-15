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
            SeedProducts(db);
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

        private static void SeedProducts(SmashHubContext db)
        {
            if (db.Products.Any()) return;

            db.Products.AddRange(
                new Product
                {
                    Title = "Yonex Astrox 88D Pro",
                    Price = 2800,
                    Description = "Профессиональная ракетка для атакующей игры, состояние отличное.",
                    Category = "rackets",
                    Condition = "used",
                    Image = "/media/images/200x200_raketki.jpg",
                    ColorLabel = "Black / Silver",
                    Fit = "unisex",
                    SellerPhone = "+37360000002"
                },
                new Product
                {
                    Title = "Victor A970 NitroLite",
                    Price = 1950,
                    Description = "Легкие кроссовки для бадминтона с хорошей фиксацией стопы.",
                    Category = "shoes",
                    Condition = "new",
                    Image = "/media/images/200x200_krossovki.jpg",
                    SizeLabel = "42",
                    ColorLabel = "White / Blue",
                    Fit = "mens",
                    SellerPhone = "+37360000001"
                },
                new Product
                {
                    Title = "Yonex BG80 Power String",
                    Price = 190,
                    Description = "Струна для перетяжки с жестким ощущением и мощным отскоком.",
                    Category = "strings",
                    Condition = "new",
                    Image = "/media/images/200x200_struna.jpg",
                    ColorLabel = "Yellow",
                    Fit = "unisex",
                    SellerPhone = "+37360000002"
                });

            db.SaveChanges();
        }
    }
}
