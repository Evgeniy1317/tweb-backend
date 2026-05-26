using SmashHub.BusinessLogic.Interfaces;
using SmashHub.DataAccess;
using SmashHub.Domain;
using SmashHub.Domain.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace SmashHub.BusinessLogic
{
    public class ProductBL : IProduct
    {
        private readonly SmashHubContext _db;

        public ProductBL(SmashHubContext db)
        {
            _db = db;
        }

        public List<ProductModel> GetAll()
        {
            return _db.Products
                .Include(product => product.ExtraImages)
                .Include(product => product.SellerContacts)
                .AsEnumerable()
                .Select(product => ToProductModel(product))
                .ToList();
        }

        public ProductModel? GetById(int id)
        {
            var product = GetEntityById(id);
            return product == null ? null : ToProductModel(product);
        }

        public ProductModel Create(ProductCreateModel model, int ownerId)
        {
            var owner = _db.Users
                .Include(user => user.Contacts)
                .FirstOrDefault(user => user.Id == ownerId);

            var product = new Product
            {
                Title = model.Title,
                Price = model.Price,
                Description = model.Description,
                Category = model.Category,
                Condition = model.Condition,
                Image = model.Image,
                SizeLabel = model.SizeLabel,
                ColorLabel = model.ColorLabel,
                Fit = model.Fit,
                SellerPhone = string.IsNullOrWhiteSpace(model.SellerPhone) ? owner?.Phone : model.SellerPhone.Trim(),
                OwnerId = ownerId,
                ExtraImages = ToProductImages(model.ExtraImages),
                SellerContacts = ToSellerContacts(model.SellerContacts, owner)
            };

            _db.Products.Add(product);
            _db.SaveChanges();
            return ToProductModel(product);
        }

        public ProductModel? Update(int id, ProductUpdateModel model)
        {
            var product = GetEntityById(id);
            if (product == null) return null;

            product.Title = model.Title;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Category = model.Category;
            product.Condition = model.Condition;
            product.Image = model.Image;
            product.SizeLabel = model.SizeLabel;
            product.ColorLabel = model.ColorLabel;
            product.Fit = model.Fit;
            product.SellerPhone = model.SellerPhone?.Trim();
            ReplaceExtraImages(product, model.ExtraImages);
            ReplaceSellerContacts(product, model.SellerContacts);

            _db.SaveChanges();
            return ToProductModel(product);
        }

        public bool Delete(int id)
        {
            var product = GetEntityById(id);
            if (product == null) return false;
            _db.Products.Remove(product);
            _db.SaveChanges();
            return true;
        }

        private Product? GetEntityById(int id)
        {
            return _db.Products
                .Include(product => product.ExtraImages)
                .Include(product => product.SellerContacts)
                .FirstOrDefault(product => product.Id == id);
        }

        private static List<ProductImage> ToProductImages(List<string>? extraImages)
        {
            return NormalizeImageUrls(extraImages)
                .Select((url, index) => new ProductImage
                {
                    Url = url,
                    SortOrder = index
                })
                .ToList();
        }

        private static List<ProductSellerContact> ToSellerContacts(
            List<SellerContactSnapshotModel>? sellerContacts,
            User? owner = null)
        {
            var sourceContacts = sellerContacts?
                .Where(contact => !string.IsNullOrWhiteSpace(contact.Platform) && !string.IsNullOrWhiteSpace(contact.Value))
                .Select(contact => new SellerContactSnapshotModel
                {
                    Platform = contact.Platform.Trim(),
                    Value = contact.Value.Trim()
                })
                .ToList();

            if (sourceContacts == null || sourceContacts.Count == 0)
            {
                sourceContacts = owner?.Contacts
                    .Where(contact => !string.IsNullOrWhiteSpace(contact.Platform) && !string.IsNullOrWhiteSpace(contact.Value))
                    .Select(contact => new SellerContactSnapshotModel
                    {
                        Platform = contact.Platform.Trim(),
                        Value = contact.Value.Trim()
                    })
                    .ToList() ?? new List<SellerContactSnapshotModel>();
            }

            return sourceContacts
                .Select(contact => new ProductSellerContact
                {
                    Platform = contact.Platform,
                    Value = contact.Value
                })
                .ToList();
        }

        private static List<string> NormalizeImageUrls(List<string>? extraImages)
        {
            return (extraImages ?? new List<string>())
                .Where(image => !string.IsNullOrWhiteSpace(image))
                .Select(image => image.Trim())
                .Take(7)
                .ToList();
        }

        private static void ReplaceExtraImages(Product product, List<string>? extraImages)
        {
            product.ExtraImages.Clear();
            product.ExtraImages.AddRange(ToProductImages(extraImages));
        }

        private static void ReplaceSellerContacts(Product product, List<SellerContactSnapshotModel>? sellerContacts)
        {
            product.SellerContacts.Clear();
            product.SellerContacts.AddRange(ToSellerContacts(sellerContacts));
        }

        private static ProductModel ToProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Condition = product.Condition,
                Image = product.Image,
                SizeLabel = product.SizeLabel,
                ColorLabel = product.ColorLabel,
                Fit = product.Fit,
                SellerPhone = product.SellerPhone,
                OwnerId = product.OwnerId,
                ExtraImages = product.ExtraImages
                    .OrderBy(image => image.SortOrder)
                    .Select(image => image.Url)
                    .ToList(),
                SellerContacts = product.SellerContacts
                    .Select(contact => new SellerContactSnapshotModel
                    {
                        Platform = contact.Platform,
                        Value = contact.Value
                    })
                    .ToList()
            };
        }
    }
}
