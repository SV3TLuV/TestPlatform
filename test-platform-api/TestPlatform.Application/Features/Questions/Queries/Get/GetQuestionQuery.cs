using MediatR;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Application.Features.Questions.Queries.Get;

public sealed record GetQuestionQuery(int Id) : IRequest<QuestionViewModel>;