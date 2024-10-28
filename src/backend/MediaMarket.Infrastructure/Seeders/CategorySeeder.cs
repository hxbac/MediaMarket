using Bogus;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using MediaMarket.Infrastructure.Extensions;

namespace MediaMarket.Infrastructure.Seeders
{
    internal class CategorySeeder : IDbSeeder<ApplicationDbContext>
    {
        public async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(getCategoriesSeed());
                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<Category> getCategoriesSeed()
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Name.JobType())
                .RuleFor(c => c.Image, f => f.Image.LoremFlickrUrl())
                .RuleFor(c => c.Description, f => f.Lorem.Word())
                .RuleFor(c => c.Order, f => f.Random.Number());

            return categoryFaker.Generate(10);
        }
    }
}
