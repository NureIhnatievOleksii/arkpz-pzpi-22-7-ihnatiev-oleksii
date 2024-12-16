using AirSense.Domain.UserAggregate;

namespace AirSense.Application.Interfaces.Services;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(User user);
}
