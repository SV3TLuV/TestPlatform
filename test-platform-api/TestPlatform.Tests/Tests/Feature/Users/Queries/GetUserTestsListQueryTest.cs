using AutoBogus;
using MediatR;
using TestPlatform.Application.Features.Users.Queries.GetUserTestsList;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Users.Queries;

public class GetUserTestsListQueryTest
{
    [Fact]
    public async Task Test_HandleGetUser()
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<GetUserTestsListQuery, ICollection<TestViewModel>>>();
        var query = new GetUserTestsListQuery(1);
        await context.Database.EnsureCreatedAsync();

        var test = new Test
        {
            Name = "Test",
            Description = "Test description",
            UserId = 1,
            Questions = new List<Question>()
        };

        await context.Set<Test>().AddAsync(test);
        await context.SaveChangesAsync();

        // Act
        var result = await handler.Handle(query, default);
        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.NotEmpty(result);
    }
    
    /*public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<Test>()
                    .RuleFor(e => e.TestId, f => count)
                    .RuleFor(e => e.UserId, f => count)
                    .RuleFor(e => e.Name, f => f.Random.String2(4, 10, "qwertyuiopasdfghjklzxcvbnm"))
                    .RuleFor(e => e.Description, f => f.Random.String2(4, 20, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                    .RuleFor(e => e.Questions, f => new List<Question>())
                    .Generate()
            });*/
}