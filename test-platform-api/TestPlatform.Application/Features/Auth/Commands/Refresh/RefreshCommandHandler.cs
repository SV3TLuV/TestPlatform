using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestPlatform.Application.Services;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Auth.Commands.Refresh;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, AuthResponseViewModel>
{
    private readonly ITestDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;

    public RefreshCommandHandler(ITestDbContext context,
        TokenService tokenService,
        IMapper mapper)
    {
        _context = context;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<AuthResponseViewModel> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var userFromDb = await _context.Set<User>()
            .FirstOrDefaultAsync(e => 
                e.Login == principal!.Identity!.Name, cancellationToken);
        
        if (userFromDb == null) 
            throw new NotFoundException(nameof(User), "Incorrect token");
        
        if (userFromDb.RefreshToken != request.RefreshToken)
            throw new SecurityTokenException("Invalid refresh token");

        var newJwtToken = _tokenService.GenerateAccessToken(userFromDb);
        var refreshToken = _tokenService.GenerateRefreshToken();
        userFromDb.RefreshToken = refreshToken;
        await _context.SaveChangesAsync(cancellationToken);

        var userViewModel = _mapper.Map<UserViewModel>(userFromDb);
        
        return new AuthResponseViewModel
        {
            Tokens = new TokenPairViewModel
            {
                AccessToken = newJwtToken,
                RefreshToken = refreshToken
            },
            User = userViewModel
        };
    }
}