using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AirSense.Application.CQRS.Dtos.Commands;
using AirSense.Application.Interfaces.Repositories;
using AirSense.Application.Interfaces.Services;
using AirSense.Application.Options;
using AirSense.Domain.UserAggregate;

namespace AirSense.Application.CQRS.Commands.Auth.Login;

public class LoginCommandHandler(
    IAuthRepository authRepository,
    UserManager<User> userManager,
    IOptions<AuthenticationOptions> authenticationOptions,
    IJwtService jwtService) : IRequestHandler<LoginCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await authRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return CreateLoginResult(false, "User not found");
        }
        if (user.IsBanned)
        {
            return CreateLoginResult(false, "User is banned.");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            return CreateLoginResult(false, "Invalid password");
        }

        var roles = await userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();  

        var token = await jwtService.GenerateJwtTokenAsync(user);

        var tokenResult = await userManager.SetAuthenticationTokenAsync(user, authenticationOptions.Value.AirSense.Provider, authenticationOptions.Value.AirSense.TokenName, token);

        if (!tokenResult.Succeeded)
        {
            return CreateLoginResult(false, "Could not save token");
        }

        return CreateLoginResult(true, token: token);
    }

    private AuthResponseDto CreateLoginResult(bool success, string errorMessage = null, string token = null)
    {
        return new AuthResponseDto { IsSuccess = success, ErrorMessage = errorMessage, Token = token };
    }
}
