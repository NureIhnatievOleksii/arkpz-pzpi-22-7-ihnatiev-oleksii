using AirSense.Application.Interfaces.Repositories;
using AirSense.Domain.UserAggregate;
using AirSense.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AirSense.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AirSenseContext _context;

        public UserRepository(AirSenseContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken)  // Изменили название метода
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);  // Теперь просто возвращаем пользователя без постов
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
