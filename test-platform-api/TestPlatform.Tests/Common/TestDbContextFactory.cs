using Microsoft.EntityFrameworkCore;
using TestPlatform.Persistence.Context;

namespace TestPlatform.Tests.Common;

internal static class TestDbContextFactory
{
    public static TestDbContext Create()
    {
        var dbContext = new TestDbContext(CreateOptions());
        dbContext.Database.EnsureCreated();
        return dbContext;
    } 
    
    public static DbContextOptions<TestDbContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
}