using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserViewModel> Handle(GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(User), request.Id);

        return _mapper.Map<UserViewModel>(user);
    }
}