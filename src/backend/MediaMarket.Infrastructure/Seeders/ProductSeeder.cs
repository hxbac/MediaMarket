using Bogus;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Seeders
{
    internal class ProductSeeder : IDbSeeder<ApplicationDbContext>
    {
        private ApplicationDbContext _context;

        public async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Products.Any())
            {
                _context = context;
                var categories = getCategories();
                var tags = getTags();
                var products = getProductSeeds();
                foreach (var product in products)
                {
                    product.Tags = tags.Take(5).ToList();
                    product.Categories = categories.Take(5).ToList();
                    context.Products.Add(product);
                }

                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<Product> getProductSeeds()
        {

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Name.JobTitle())
                .RuleFor(p => p.Thumbnail, f => f.Image.LoremFlickrUrl())
                .RuleFor(p => p.ShortDescription, f => f.Lorem.Text())
                .RuleFor(p => p.Description, f => f.Lorem.Paragraphs())
                .RuleFor(p => p.Price, f => f.Random.Int(100000))
                .RuleFor(p => p.ProductType, f => f.PickRandom<ProductType>())
                .RuleFor(p => p.ProductStatus, f => f.PickRandom<ProductStatus>())
                .RuleFor(p => p.ProductContentStatus, f => f.PickRandom<ProductContentStatus>());

            return productFaker.Generate(30);
        }

        private IEnumerable<Category> getCategories()
        {
            return _context.Categories.ToList();
        }

        private ICollection<Tag> getTags()
        {
            return _context.Tags.ToList();
        }
    }
}
