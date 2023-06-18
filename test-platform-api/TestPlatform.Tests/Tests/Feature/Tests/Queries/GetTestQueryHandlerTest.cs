using AutoBogus;
using MediatR;
using TestPlatform.Application.Features.Tests.Queries.Get;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Tests.Queries;

public class GetTestQueryHandlerTest
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task Test_Handle_GetTest(
        GetTestQuery query)
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<GetTestQuery, TestViewModel>>();

        await context.Database.EnsureCreatedAsync();

        var test = new Test
        {
            TestId = query.Id,
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
        Assert.NotNull(result);
        Assert.Equal(test.TestId, result.Id);
    }
    
    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<GetTestQuery>()
                    .RuleFor(e => e.Id, f => f.UniqueIndex)
                    .Generate()
            });
}