using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests;

public class DbContextFactory
{
    public static AppDbContext CreateDbContext(string databaseName)
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        AppDbContext context = new (options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        List<TagEntity> tags =
        [
            new TagEntity
            {
                Name = "Phone"
            },
            new TagEntity
            {
                Name = "PC"
            },
            new TagEntity
            {
                Name = "Mac"
            }
        ];

        context.Tags.AddRange(tags);

        context.SaveChanges();

        return context;
    }
}
