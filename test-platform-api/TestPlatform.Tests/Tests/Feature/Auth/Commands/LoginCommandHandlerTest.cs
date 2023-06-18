using AutoBogus;
using MediatR;
using TestPlatform.Application.Common.Interfaces;
using TestPlatform.Application.Features.Auth.Commands.Login;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Auth.Commands;

public class LoginCommandHandlerTest
{
    [Fact]
    public async Task Auth_Handle_LoginTest()
    {
        // Arrange
        var context = TestContainer.Resolve<ITestDbContext>();
        var hasher = TestContainer.Resolve<IPasswordHasher>();
        var handler = TestContainer.Resolve<IRequestHandler<LoginCommand, AuthResponseViewModel>>();

        //await context.Database.EnsureCreatedAsync();
        await context.Set<User>().AddAsync(new User
        {
            Login = "test",
            Password = hasher.Hash("test")
        });
        await context.SaveChangesAsync();
        
        var command = new LoginCommand
        {
            Login = "test",
            Password = "test"
        };

        // Act
        var result = await handler.Handle(command, default);
        //await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.Equal(result.User.Login, command.Login);
    }
}