using Bogus;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using MediaMarket.Infrastructure.Extensions;

namespace MediaMarket.Infrastructure.Seeders
{
    internal class TagSeeder : IDbSeeder<ApplicationDbContext>
    {
        public async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Tags.Any())
            {
                context.Tags.AddRange(getTagsSeed());
                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<Tag> getTagsSeed()
        {
            var tagFaker = new Faker<Tag>()
                .RuleFor(c => c.Name, f => f.Name.JobType());

            return tagFaker.Generate(10);
        }
    }
}
