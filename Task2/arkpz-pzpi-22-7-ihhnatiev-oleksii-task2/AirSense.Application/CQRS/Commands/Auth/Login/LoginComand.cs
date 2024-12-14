using MediatR;
using AirSense.Application.CQRS.Dtos.Commands;

namespace AirSense.Application.CQRS.Commands.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
