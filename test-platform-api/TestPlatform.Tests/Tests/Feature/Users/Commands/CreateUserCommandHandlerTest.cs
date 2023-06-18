using AutoBogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Features.Users.Commands.Create;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Users.Commands;

public class CreateUserCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task User_Handle_AddUsers(
        CreateUserCommand command)
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<CreateUserCommand, int>>();
        await context.Database.EnsureCreatedAsync();

        // Act
        await handler.Handle(command, default);

        var userDbo = await context
            .Set<User>()
            .ToArrayAsync();

        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.Contains(userDbo, u => u.Login == command.Login);
    }

    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<CreateUserCommand>()
                    .RuleFor(e => e.Login, f => f.Random
                        .String2(4, 10, "qwertyuiopasdfghjklzxcvbnm"))
                    .RuleFor(e => e.Password, f => f.Random
                        .String2(4, 20, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                    .Generate()
            });
}