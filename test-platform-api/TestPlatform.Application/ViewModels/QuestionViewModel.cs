using AutoMapper;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.ViewModels;

public class QuestionViewModel : IMapWith<Question>
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int TestId { get; set; }
    public IEnumerable<AnswerViewModel> Answers { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Question, QuestionViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(question => question.QuestionId))
            .ReverseMap();
    }
}