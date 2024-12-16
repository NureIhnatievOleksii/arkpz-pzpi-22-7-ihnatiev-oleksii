using MediatR;
using AirSense.Application.CQRS.Dtos.Commands;

namespace AirSense.Application.CQRS.Commands.Auth.Register;

public record RegisterCommand(string UserName, string Email, string Password) : IRequest<AuthResponseDto>;
