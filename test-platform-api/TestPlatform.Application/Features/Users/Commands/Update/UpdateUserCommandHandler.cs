using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Common.Interfaces;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly ITestDbContext _context;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(ITestDbContext context,
        IPasswordHasher hasher,
        IMapper mapper)
    {
        _context = context;
        _hasher = hasher;
        _mapper = mapper;
    }

    public async Task Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userDto = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken);

        if (userDto is null)
            throw new NotFoundException(nameof(User), request.Id);

        var user = _mapper.Map<User>(request);
        user.Password = _hasher.Hash(request.Password);

        _context.Set<User>().Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}