using MediatR;
using Microsoft.AspNetCore.Identity;
using AirSense.Domain.UserAggregate;
using AirSense.Application.CQRS.Commands.Admins.BanUser;

namespace AirSense.Application.CQRS.Commands.Admin.BanUser;

public class BanUserCommandHandler(UserManager<User> userManager) : IRequestHandler<BanUserCommand, bool>
{
    public async Task<bool> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user == null)
        {
            return false; 
        }

        user.IsBanned = request.IsBanned;
        var result = await userManager.UpdateAsync(user);

        return result.Succeeded;
    }
}
