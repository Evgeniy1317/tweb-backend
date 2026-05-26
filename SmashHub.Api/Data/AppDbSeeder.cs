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

            SeedUserContacts(db, "eugeniy@smashhub.local", new[]
            {
                ("telegram", "eugeniy_smashhub"),
                ("instagram", "eugeniy.smashhub"),
                ("viber", "+37360000001"),
                ("facebook", "eugeniy.smashhub"),
                ("whatsapp", "+37360000001")
            });

            SeedUserContacts(db, "anzor@smashhub.local", new[]
            {
                ("telegram", "anzor_smashhub"),
                ("instagram", "anzor.smashhub"),
                ("viber", "+37360000002"),
                ("facebook", "anzor.smashhub"),
                ("whatsapp", "+37360000002")
            });

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

        private static void SeedUserContacts(
            SmashHubContext db,
            string email,
            IEnumerable<(string Platform, string Value)> contacts)
        {
            var user = db.Users
                .Include(u => u.Contacts)
                .FirstOrDefault(u => u.Email == email);

            if (user == null) return;

            foreach (var contact in contacts)
            {
                if (user.Contacts.Any(c => c.Platform == contact.Platform)) continue;

                user.Contacts.Add(new UserContact
                {
                    Platform = contact.Platform,
                    Value = contact.Value,
                    UserId = user.Id
                });
            }
        }

        private static void SeedProducts(SmashHubContext db)
        {
            var admin = db.Users
                .Include(u => u.Contacts)
                .FirstOrDefault(u => u.Email == "eugeniy@smashhub.local");
            var manager = db.Users
                .Include(u => u.Contacts)
                .FirstOrDefault(u => u.Email == "anzor@smashhub.local");

            if (db.Products.Any())
            {
                BackfillProductOwners(db, admin, manager);
                return;
            }

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
                    SellerPhone = "+37360000002",
                    OwnerId = manager?.Id ?? admin?.Id,
                    SellerContacts = SnapshotContacts(manager ?? admin)
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
                    SellerPhone = "+37360000001",
                    OwnerId = admin?.Id ?? manager?.Id,
                    SellerContacts = SnapshotContacts(admin ?? manager)
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
                    SellerPhone = "+37360000002",
                    OwnerId = manager?.Id ?? admin?.Id,
                    SellerContacts = SnapshotContacts(manager ?? admin)
                });

            db.SaveChanges();
        }

        private static void BackfillProductOwners(SmashHubContext db, User? admin, User? manager)
        {
            var productsWithoutOwner = db.Products.Where(p => p.OwnerId == null).ToList();
            if (productsWithoutOwner.Count > 0)
            {
                foreach (var product in productsWithoutOwner)
                {
                    product.OwnerId = product.SellerPhone == "+37360000001"
                        ? admin?.Id ?? manager?.Id
                        : manager?.Id ?? admin?.Id;
                }

                db.SaveChanges();
            }

            BackfillProductSellerContacts(db);
        }

        private static void BackfillProductSellerContacts(SmashHubContext db)
        {
            var productsWithoutContacts = db.Products
                .Include(p => p.Owner)
                .ThenInclude(u => u!.Contacts)
                .Include(p => p.SellerContacts)
                .Where(p => !p.SellerContacts.Any())
                .ToList();

            foreach (var product in productsWithoutContacts)
            {
                product.SellerContacts = SnapshotContacts(product.Owner);
            }

            db.SaveChanges();
        }

        private static List<ProductSellerContact> SnapshotContacts(User? user)
        {
            return user?.Contacts
                .Where(contact => !string.IsNullOrWhiteSpace(contact.Platform) && !string.IsNullOrWhiteSpace(contact.Value))
                .Select(contact => new ProductSellerContact
                {
                    Platform = contact.Platform.Trim(),
                    Value = contact.Value.Trim()
                })
                .ToList() ?? new List<ProductSellerContact>();
        }
    }
}
