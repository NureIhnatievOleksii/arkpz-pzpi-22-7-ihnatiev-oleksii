using MediatR;

namespace AirSense.Application.CQRS.Commands.Admins.AssignAdminRole
{
    public record AssignAdminRoleCommand(Guid UserId) : IRequest<CommandResultDto>;
}
public class CommandResultDto
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }

    public CommandResultDto(bool isSuccess, string errorMessage = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }
}