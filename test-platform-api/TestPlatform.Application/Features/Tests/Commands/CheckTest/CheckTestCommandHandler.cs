using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Commands.CheckTest;

public sealed class CheckTestCommandHandler 
    : IRequestHandler<CheckTestCommand, TestResultViewModel>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public CheckTestCommandHandler(ITestDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TestResultViewModel> Handle(CheckTestCommand request,
        CancellationToken cancellationToken)
    {
        var test = await _context.Set<Test>()
            .Include(e => e.Questions)
            .ThenInclude(e => e.Answers)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TestId == request.TestId, cancellationToken);

        if (test is null)
            throw new NotFoundException(nameof(Test), request.TestId);

        var rightAnswersCount = 0;

        var requestQuestions = _mapper.Map<Question[]>(request.Questions);
        
        foreach (var question in requestQuestions)
        {
            var rightAnswers = await _context.Set<Answer>()
                .AsNoTrackingWithIdentityResolution()
                .Where(e => 
                    e.QuestionId == question.QuestionId &&
                    e.TestId == question.TestId &&
                    e.IsRight)
                .Select(e => e.AnswerId)
                .ToArrayAsync(cancellationToken);

            var userAnswers = question.Answers
                .Where(e => e.IsRight)
                .Select(e => e.AnswerId)
                .ToArray();
            
            if (rightAnswers.SequenceEqual(userAnswers))
                rightAnswersCount++;
        }
        
        return new TestResultViewModel
        {
            TestId = request.TestId,
            CountRightAnswers = rightAnswersCount,
            CountAnswers = test.Questions.Count
        };
    }
}