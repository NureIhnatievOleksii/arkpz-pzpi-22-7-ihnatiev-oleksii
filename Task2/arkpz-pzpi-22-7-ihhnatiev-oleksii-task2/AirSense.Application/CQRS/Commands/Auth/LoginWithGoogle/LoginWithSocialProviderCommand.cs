using MediatR;
using AirSense.Application.CQRS.Dtos.Commands;

namespace AirSense.Application.CQRS.Commands.Auth.Login
{
    public record LoginWithGoogleCommand(
        string UserId,
        string Token
    ) : IRequest<AuthResponseDto>;
}
