using AutoMapper;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.ViewModels;

public class TestViewModel : IMapWith<Test>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
    public IEnumerable<QuestionViewModel> Questions { get; } = new List<QuestionViewModel>();

    public void Map(Profile profile)
    {
        profile.CreateMap<Test, TestViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(test => test.TestId))
            .ReverseMap();

        profile.CreateMap<Test, TestViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(test => test.TestId));
    }
}