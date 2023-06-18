using MediatR;
using TestPlatform.Application.Features.Base.Queries;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Queries.GetList;

public sealed record GetAnswerListQuery 
    : PaginatedQuery, IRequest<PagedList<AnswerViewModel>>;