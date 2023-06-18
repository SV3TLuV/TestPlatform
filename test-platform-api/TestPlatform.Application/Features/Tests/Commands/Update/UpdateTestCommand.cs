using AutoMapper;
using MediatR;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Commands.Update;

public class UpdateTestCommand : IRequest, IMapWith<Test>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
    public ICollection<Question> Questions { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Test, UpdateTestCommand>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(answer => answer.TestId))
            .ReverseMap();
    }
}