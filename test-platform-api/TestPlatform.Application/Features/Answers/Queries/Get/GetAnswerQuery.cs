using MediatR;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Application.Features.Answers.Queries.Get;

public sealed record GetAnswerQuery(int Id) : IRequest<AnswerViewModel>;