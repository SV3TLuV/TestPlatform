using AutoBogus;
using MediatR;
using TestPlatform.Application.Features.Users.Queries.GetList;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Users.Queries;

public class GetUserListQueryTest
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task Test_Handle_GetUser(
        User user)
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<GetUserListQuery, ICollection<UserViewModel>>>();
        var query = new GetUserListQuery();

        await context.Database.EnsureCreatedAsync();
        await context.Set<User>().AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        var result = await handler.Handle(query, default);
        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.NotEmpty(result);
    }
    
    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<User>()
                    .RuleFor(e => e.UserId, f => f.UniqueIndex)
                    .RuleFor(e => e.Login, f => f.Random
                        .String2(4, 10, "qwertyuiopasdfghjklzxcvbnm"))
                    .RuleFor(e => e.Password, f => f.Random
                        .String2(4, 20, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                    .Generate()
            });
}