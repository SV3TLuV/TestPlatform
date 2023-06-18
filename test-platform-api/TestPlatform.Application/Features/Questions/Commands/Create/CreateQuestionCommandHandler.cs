using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Commands.Create;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public CreateQuestionCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var question = _mapper.Map<Question>(request);
        await _context.Set<Question>().AddAsync(question, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return question.QuestionId;
    }
}