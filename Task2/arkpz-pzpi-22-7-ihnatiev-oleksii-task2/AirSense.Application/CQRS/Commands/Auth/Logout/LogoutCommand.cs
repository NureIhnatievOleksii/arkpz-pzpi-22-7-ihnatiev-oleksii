using MediatR;
using AirSense.Application.CQRS.Dtos.Commands;

namespace AirSense.Application.CQRS.Commands.Auth.Logout;

public record LogoutCommand(string Token) : IRequest<AuthResponseDto>;
