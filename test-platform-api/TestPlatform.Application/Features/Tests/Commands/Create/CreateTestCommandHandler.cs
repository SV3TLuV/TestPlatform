using AutoMapper;
using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Commands.Create;

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, int>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public CreateTestCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTestCommand request,
        CancellationToken cancellationToken)
    {
        var test = _mapper.Map<Test>(request);
        await _context.Set<Test>().AddAsync(test, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return test.TestId;
    }
}