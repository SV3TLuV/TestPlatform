using MediatR;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;

namespace TestPlatform.Application.Features.Auth.Commands.Refresh;

public class RefreshCommand : IMapWith<TokenPairViewModel>, IRequest<AuthResponseViewModel>
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}