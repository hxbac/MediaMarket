using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

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

        private List<Category> getCategoriesSeed()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kinh doanh",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/graphic_design.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Giải trí",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/fine_art.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Marketing",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/ui_ux_design.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nghệ thuật",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/animation.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sáng tạo",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/film_video.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Thể thao",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/graphic_design.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sức khỏe",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/marketing.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hội họa",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/animation.webp",
                    Order = 9999,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hội họa",
                    Description = "Kinh doanh",
                    Image = "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/animation.webp",
                    Order = 9999,
                }
            };
        }
    }
}
