using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Queries.GetList;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ICollection<UserViewModel>>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetUserListQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<UserViewModel>> Handle(GetUserListQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        return _mapper.Map<UserViewModel[]>(users);
    }
}