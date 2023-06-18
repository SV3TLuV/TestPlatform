using AutoMapper;
using MediatR;
using TestPlatform.Application.Common.Interfaces;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly ITestDbContext _context;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(ITestDbContext context,
        IPasswordHasher hasher,
        IMapper mapper)
    {
        _context = context;
        _hasher = hasher;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        user.Password = _hasher.Hash(request.Password);
        await _context.Set<User>().AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user.UserId;
    }
}