using AutoMapper;
using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Commands.Update;

public class UpdateQuestionCommand : IRequest, IMapWith<Question>
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int TestId { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Question, UpdateQuestionCommand>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(answer => answer.QuestionId))
            .ReverseMap();
    }
}