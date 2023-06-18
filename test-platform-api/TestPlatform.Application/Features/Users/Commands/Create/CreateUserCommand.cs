using AutoMapper;
using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommand : IRequest<int>, IMapWith<User>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<User, CreateUserCommand>()
            .ForMember(command => command.Login, expression =>
                expression.MapFrom(user => user.Login))
            .ForMember(command => command.Password, expression =>
                expression.MapFrom(user => user.Password));

        profile.CreateMap<CreateUserCommand, User>()
            .ForMember(user => user.Login, expression =>
                expression.MapFrom(command => command.Login))
            .ForMember(user => user.Password, expression =>
                expression.MapFrom(command => command.Password));
    }
}