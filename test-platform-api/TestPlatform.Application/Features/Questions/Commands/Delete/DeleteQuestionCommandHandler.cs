using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Commands.Delete;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public DeleteQuestionCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(DeleteQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var question = await _context.Set<Question>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.QuestionId == request.Id, cancellationToken);

        if (question is null)
            throw new NotFoundException(nameof(Question), request.Id);

        _context.Set<Question>().Remove(question);
        await _context.SaveChangesAsync(cancellationToken);
    }
}