using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(User), request.Id);

        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}