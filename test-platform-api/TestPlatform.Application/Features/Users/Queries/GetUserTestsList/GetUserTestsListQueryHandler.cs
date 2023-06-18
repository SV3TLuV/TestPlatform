using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Queries.GetUserTestsList;

public class GetUserTestsListQueryHandler : IRequestHandler<GetUserTestsListQuery, ICollection<TestViewModel>>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetUserTestsListQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<TestViewModel>> Handle(GetUserTestsListQuery request,
        CancellationToken cancellationToken)
    {
        var tests = await _context.Set<Test>()
            .AsNoTrackingWithIdentityResolution()
            .Include(test => test.Questions)
            .ThenInclude(question => question.Answers)
            .Where(e => e.UserId == request.Id)
            .ToListAsync(cancellationToken);

        return _mapper.Map<TestViewModel[]>(tests);
    }
}