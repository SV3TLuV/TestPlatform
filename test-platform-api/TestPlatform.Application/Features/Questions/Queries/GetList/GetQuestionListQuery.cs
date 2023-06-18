using MediatR;
using TestPlatform.Application.Features.Base.Queries;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Queries.GetList;

public sealed record GetQuestionListQuery 
    : PaginatedQuery, IRequest<PagedList<QuestionViewModel>>;