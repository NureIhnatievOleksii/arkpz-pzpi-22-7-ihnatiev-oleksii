using AirSense.Domain.UserAggregate;

namespace AirSense.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken);  // Изменили название метода
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
    }
}
