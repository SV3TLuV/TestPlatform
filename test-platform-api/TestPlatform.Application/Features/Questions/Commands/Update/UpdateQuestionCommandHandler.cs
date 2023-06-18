using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Features.Answers.Commands.Update;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Commands.Update;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public UpdateQuestionCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var questionDto = await _context.Set<Question>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.QuestionId == request.Id, cancellationToken);

        if (questionDto is null)
            throw new NotFoundException(nameof(Question), request.Id);

        var question = _mapper.Map<Question>(request);
        
        _context.Set<Question>().Update(question);
        await _context.SaveChangesAsync(cancellationToken);
    }
}