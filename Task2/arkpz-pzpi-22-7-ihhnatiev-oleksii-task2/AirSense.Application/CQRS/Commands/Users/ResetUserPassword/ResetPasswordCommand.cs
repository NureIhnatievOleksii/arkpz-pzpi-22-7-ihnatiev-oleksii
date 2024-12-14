using MediatR;

namespace AirSense.Application.CQRS.Commands.Users.ResetUserPassword;

public record ResetUserPasswordCommand(Guid UserId, string NewPassword) : IRequest;
