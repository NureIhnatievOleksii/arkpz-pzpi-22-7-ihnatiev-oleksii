using AirSense.Application.CQRS.Dtos.Commands;
using MediatR;

namespace AirSense.Application.CQRS.Commands.Auth
{
    public class LoginWithGitHubCommand : IRequest<AuthResponseDto>
    {
        public string Code { get; set; }
    }
}