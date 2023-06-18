using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Queries.Get;

public class GetAnswerQueryHandler : IRequestHandler<GetAnswerQuery, AnswerViewModel>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetAnswerQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AnswerViewModel> Handle(GetAnswerQuery request,
        CancellationToken cancellationToken)
    {
        var answer = await _context.Set<Answer>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.AnswerId == request.Id, cancellationToken);

        if (answer is null)
            throw new NotFoundException(nameof(answer), request.Id);

        return _mapper.Map<AnswerViewModel>(answer);
    }
}