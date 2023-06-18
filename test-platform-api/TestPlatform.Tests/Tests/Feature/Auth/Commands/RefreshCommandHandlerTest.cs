using MediatR;
using TestPlatform.Application.Features.Auth.Commands.Refresh;
using TestPlatform.Application.ViewModels;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Auth.Commands;

public class RefreshCommandHandlerTest
{
    [Fact]
    public async Task Auth_Handle_RefreshTest()
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<RefreshCommand, AuthResponseViewModel>>();
        await context.Database.EnsureCreatedAsync();
        
        var command = new RefreshCommand
        {
            AccessToken = null,
            RefreshToken = null
        };

        // Act
        var result = await handler.Handle(command, default);
        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.Equal(result.Tokens.AccessToken, command.AccessToken);
    }
}