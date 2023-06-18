using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Queries.Get;

public class GetTestQueryHandler : IRequestHandler<GetTestQuery, TestViewModel>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetTestQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TestViewModel> Handle(GetTestQuery request,
        CancellationToken cancellationToken)
    {
        var test = await _context.Set<Test>()
            .AsNoTrackingWithIdentityResolution()
            .Include(test => test.Questions)
            .ThenInclude(question => question.Answers)
            .FirstOrDefaultAsync(e => e.TestId == request.Id, cancellationToken);

        if (test is null)
            throw new NotFoundException(nameof(Test), request.Id);

        foreach (var question in test.Questions)
            foreach (var answer in question.Answers)
                answer.IsRight = false;
        
        return _mapper.Map<TestViewModel>(test);
    }
}