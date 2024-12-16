namespace AirSense.Application.CQRS.Dtos.Queries
{
    public record GetAllUsersQueryDto
(
    Guid Id,
    string Username,
    string? Photo
);

}
