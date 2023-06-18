using AutoMapper;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.ViewModels;

public class AnswerViewModel : IMapWith<Answer>
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int QuestionId { get; set; }
    public int TestId { get; set; }
    public bool IsRight { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Answer, AnswerViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(answer => answer.AnswerId))
            .ReverseMap();
    }
}