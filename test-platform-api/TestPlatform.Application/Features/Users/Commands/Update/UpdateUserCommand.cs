using AutoMapper;
using MediatR;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest, IMapWith<User>
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<User, UpdateUserCommand>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(answer => answer.UserId))
            .ReverseMap();
    }
}