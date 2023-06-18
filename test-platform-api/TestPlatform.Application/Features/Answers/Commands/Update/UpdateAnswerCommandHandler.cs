using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Commands.Update;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public UpdateAnswerCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        var answerDto = await _context.Set<Answer>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.AnswerId == request.Id, cancellationToken);

        if (answerDto is null)
            throw new NotFoundException(nameof(Answer), request.Id);

        var answer = _mapper.Map<Answer>(request);
        _context.Set<Answer>().Update(answer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}