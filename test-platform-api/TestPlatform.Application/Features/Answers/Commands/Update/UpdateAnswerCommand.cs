using AutoMapper;
using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Commands.Update;

public class UpdateAnswerCommand : IRequest, IMapWith<Answer>
{
    public required int Id { get; set; }
    public string Text { get; set; } = null!;
    public bool IsRight { get; set; }
    public int QuestionId { get; set; }
    public int TestId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Answer, UpdateAnswerCommand>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(answer => answer.AnswerId))
            .ReverseMap();
    }
}