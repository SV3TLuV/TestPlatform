using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Queries.GetList;

public class GetQuestionListQueryHandler 
    : IRequestHandler<GetQuestionListQuery, PagedList<QuestionViewModel>>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionListQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<QuestionViewModel>> Handle(GetQuestionListQuery request,
        CancellationToken cancellationToken)
    {
        var questions = await _context.Set<Question>()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Answers)
            .ToListAsync(cancellationToken: cancellationToken);

        var viewModels = _mapper.Map<QuestionViewModel[]>(questions);
        var totalCount = await _context.Set<Question>().CountAsync(cancellationToken);

        return new PagedList<QuestionViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}