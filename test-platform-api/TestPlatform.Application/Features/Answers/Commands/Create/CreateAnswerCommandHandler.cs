using AutoMapper;
using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Commands.Create;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, int>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public CreateAnswerCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = _mapper.Map<Answer>(request);
        await _context.Set<Answer>().AddAsync(answer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return answer.AnswerId;
    }
}