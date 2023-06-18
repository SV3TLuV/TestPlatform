using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Queries.GetList;

public class GetTestListQueryHandler 
    : IRequestHandler<GetTestListQuery, PagedList<TestViewModel>>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetTestListQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TestViewModel>> Handle(GetTestListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Test>()
            .Where(e => e.TestId > 1)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(test => test.Questions)
            .ThenInclude(question => question.Answers)
            .AsNoTrackingWithIdentityResolution();

        if (request.SearchedText is not null)
        {
            query = query.Where(e => e.Name.Contains(request.SearchedText));
        }
        
        var tests = await query.ToListAsync(cancellationToken);
        
        foreach (var test in tests)
            foreach (var question in test.Questions)
                foreach (var answer in question.Answers)
                    answer.IsRight = false;
        
        var viewModels = _mapper.Map<TestViewModel[]>(tests);
        var totalCount = await _context.Set<Test>().CountAsync(cancellationToken);

        return new PagedList<TestViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}