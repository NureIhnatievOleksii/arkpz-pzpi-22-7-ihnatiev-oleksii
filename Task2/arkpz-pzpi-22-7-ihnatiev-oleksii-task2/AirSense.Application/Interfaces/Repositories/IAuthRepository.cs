﻿using Microsoft.AspNetCore.Identity;
using AirSense.Domain.UserAggregate;

namespace AirSense.Application.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo, CancellationToken cancellationToken);
    Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken);
    Task<IdentityResult> RemoveTokenAsync(string token, CancellationToken cancellationToken);
}
