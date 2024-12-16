using AirSense.Application.CQRS.Commands.Admins.AssignAdminRole; // Provides the AssignAdminRoleCommand functionality.
using AirSense.Application.CQRS.Commands.Admins.BanUser; // Provides the BanUserCommand functionality.
using AirSense.Application.CQRS.Queries.Admins; // Provides queries related to admin operations.
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirSense.Api.Controllers
{
    [Route("api/admin")]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        [HttpPost("assign-admin-role")]
        [Authorize(Roles = "Admin")] // Restricts access to users with the "Admin" role.
        public async Task<IActionResult> AssignAdminRole([FromBody] AssignAdminRoleCommand command, CancellationToken cancellationToken)
        {
            // Assigns an admin role to a user based on the provided command.
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Admin role assigned successfully." });
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("user")]
        [Authorize(Roles = "Admin")] // Ensures only admins can retrieve user data.
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            // Retrieves all users using the GetAllUsersQuery.
            var query = new GetAllUsersQuery();
            var users = await mediator.Send(query, cancellationToken);

            return Ok(users);
        }

        [HttpPost("ban/{userId}")]
        [Authorize(Roles = "Admin")] // Only admins can ban or unban users.
        public async Task<IActionResult> BanUser(Guid userId, [FromQuery] bool isBanned, CancellationToken cancellationToken)
        {
            // Bans or unbans a user based on the isBanned parameter.
            var result = await mediator.Send(new BanUserCommand(userId, isBanned), cancellationToken);

            if (result)
            {
                return Ok(new { Message = isBanned ? "User banned successfully." : "User unbanned successfully." });
            }

            return NotFound(new { Message = "User not found." });
        }
    }
}
