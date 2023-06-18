using AutoMapper;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.ViewModels;

public class UserViewModel : IMapWith<User>
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<User, UserViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(user => user.UserId))
            .ReverseMap();
    }
}