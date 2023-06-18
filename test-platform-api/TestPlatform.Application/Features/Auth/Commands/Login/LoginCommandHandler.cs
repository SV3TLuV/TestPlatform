using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Common.Interfaces;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseViewModel>
{
    private readonly ITestDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;

    public LoginCommandHandler(ITestDbContext context,
        ITokenService tokenService,
        IPasswordHasher hasher,
        IMapper mapper)
    {
        _context = context;
        _tokenService = tokenService;
        _hasher = hasher;
        _mapper = mapper;
    }

    public async Task<AuthResponseViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await _context.Set<User>()
            .FirstOrDefaultAsync(entity =>
                    entity.Login == request.Login,
                cancellationToken);
        
        if (userFromDb is null) 
            throw new NotFoundException(nameof(User), "Incorrect login or password");
        
        if (!_hasher.Check(userFromDb.Password, request.Password))
            throw new NotFoundException(nameof(User), "Incorrect login or password");
        
        var token = _tokenService.GenerateAccessToken(userFromDb);
        var refreshToken = _tokenService.GenerateRefreshToken();
        userFromDb.RefreshToken = refreshToken;
        await _context.SaveChangesAsync(cancellationToken);

        var userViewModel = _mapper.Map<UserViewModel>(userFromDb);
        
        return new AuthResponseViewModel
        {
            Tokens = new()
            {
                AccessToken = token,
                RefreshToken = refreshToken
            },
            User = userViewModel
        };
    }
}