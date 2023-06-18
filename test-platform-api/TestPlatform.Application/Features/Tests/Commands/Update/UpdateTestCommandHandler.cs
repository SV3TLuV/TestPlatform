using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Commands.Update;

public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTestCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateTestCommand request,
        CancellationToken cancellationToken)
    {
        var testDto = await _context.Set<Test>()
            .Include(e => e.Questions)
            .ThenInclude(e => e.Answers)
            .FirstOrDefaultAsync(e => e.TestId == request.Id, cancellationToken);

        if (testDto is null)
            throw new NotFoundException(nameof(Test), request.Id);

        testDto.Name = request.Name;
        testDto.Description = request.Description;
        testDto.Questions = request.Questions;

        foreach (var question in testDto.Questions)
        {
            foreach (var answer in question.Answers)
            {
                answer.QuestionId = question.QuestionId;
                answer.TestId = question.TestId;
                _context.Set<Answer>().Add(answer);
            }

            _context.Set<Question>().Add(question);
        }
        
        _context.Set<Test>().Update(testDto);
        await _context.SaveChangesAsync(cancellationToken);
    }
}