using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Features.Users.Commands.Delete;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Users.Commands;

public class DeleteUserCommandHandler
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task User_Handle_DeleteUser(
        DeleteUserCommand command)
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<DeleteUserCommand>>();
        await context.Database.EnsureCreatedAsync();
        
        var user = new User
        {
            UserId = command.Id,
            Login = "TestLogin",
            Password = "TestPassword"
        };

        await context.Set<User>().AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        await handler.Handle(command, default);
        var result = await context.Set<User>().ToArrayAsync();
        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task Handle_NonExistingUser_ThrowsNotFoundException()
    {
        // Arrange
        var command = new DeleteUserCommand(1);
        var handler = TestContainer.Resolve<IRequestHandler<DeleteUserCommand>>();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new DeleteUserCommand(count)
            });
}