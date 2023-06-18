using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Commands.Delete;

public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public DeleteTestCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(DeleteTestCommand request,
        CancellationToken cancellationToken)
    {
        var test = await _context.Set<Test>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TestId == request.Id, cancellationToken);

        if (test is null)
            throw new NotFoundException(nameof(Test), request.Id);

        _context.Set<Test>().Remove(test);
        await _context.SaveChangesAsync(cancellationToken);
    }
}