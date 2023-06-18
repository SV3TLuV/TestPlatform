using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Commands.Delete;

public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand>
{
    private ITestDbContext _context;

    public DeleteAnswerCommandHandler(ITestDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.Set<Answer>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.AnswerId == request.Id, cancellationToken);

        if (answer is null)
            throw new NotFoundException(nameof(Answer), request.Id);

        _context.Set<Answer>().Remove(answer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}