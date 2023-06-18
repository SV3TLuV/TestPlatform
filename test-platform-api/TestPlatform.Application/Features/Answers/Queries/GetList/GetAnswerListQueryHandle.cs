using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Queries.GetList;

public class GetAnswerListQueryHandle 
    : IRequestHandler<GetAnswerListQuery, PagedList<AnswerViewModel>>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetAnswerListQueryHandle(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<AnswerViewModel>> Handle(GetAnswerListQuery request,
        CancellationToken cancellationToken)
    {
        var answers = await _context.Set<Answer>()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<AnswerViewModel[]>(answers);
        var totalCount = await _context.Set<Answer>().CountAsync(cancellationToken);

        return new PagedList<AnswerViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}