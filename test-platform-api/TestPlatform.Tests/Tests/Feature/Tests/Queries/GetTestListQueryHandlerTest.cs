using MediatR;
using TestPlatform.Application.Features.Tests.Queries.GetList;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Tests.Queries;

public class GetTestListQueryHandlerTest
{
    [Fact]
    public async Task Test_Handle_GetTestsList()
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<GetTestListQuery, PagedList<TestViewModel>>>();
        var query = new GetTestListQuery();
        await context.Database.EnsureCreatedAsync();

        var test = new[]
        {
            new Test
            {
                TestId = 1,
                Name = "Test 1",
                Description = "Test description 1",
                UserId = 1,
                Questions = new List<Question>()
            },
            new Test
            {
                TestId = 2,
                Name = "Test 2",
                Description = "Test description 2",
                UserId = 1,
                Questions = new List<Question>()
            },
            new Test
            {
                TestId = 3,
                Name = "Test 3",
                Description = "Test description 3",
                UserId = 1,
                Questions = new List<Question>()
            }
        };
        
        await context.Set<Test>().AddRangeAsync(test);
        await context.SaveChangesAsync();

        // Act
        var result = await handler.Handle(query, default);
        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.NotNull(result);
    }
}