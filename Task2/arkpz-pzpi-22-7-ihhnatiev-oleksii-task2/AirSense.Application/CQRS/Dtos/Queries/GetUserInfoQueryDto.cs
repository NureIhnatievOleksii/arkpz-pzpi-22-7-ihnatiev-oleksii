namespace AirSense.Application.CQRS.Dtos.Queries
{
    // Обновленный DTO для получения информации о пользователе
    public record GetUserInfoQueryDto
    (
        string Username,
        string Email,
        string Photo
    );

    // DTO для постов и комментариев больше не требуется, так как мы их больше не передаем
}
