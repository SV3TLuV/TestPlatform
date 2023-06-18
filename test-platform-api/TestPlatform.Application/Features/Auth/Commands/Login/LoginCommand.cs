using MediatR;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<AuthResponseViewModel>
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}