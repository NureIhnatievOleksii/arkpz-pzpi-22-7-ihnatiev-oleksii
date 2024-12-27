using AirSense.Application.CQRS.Commands.Admins.AssignAdminRole;
using AirSense.Application.CQRS.Commands.Admins.BanUser;
using AirSense.Application.CQRS.Queries.Admins;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirSense.Api.Controllers
{
    [Route("api/admin")]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        [HttpPost("assign-admin-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignAdminRole([FromBody] AssignAdminRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Admin role assigned successfully." });
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var query = new GetAllUsersQuery();
            var users = await mediator.Send(query, cancellationToken);

            return Ok(users);
        }
        [HttpPost("ban/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BanUser(Guid userId, [FromQuery] bool isBanned, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new BanUserCommand(userId, isBanned), cancellationToken);

            if (result)
            {
                return Ok(new { Message = isBanned ? "User banned successfully." : "User unbanned successfully." });
            }

            return NotFound(new { Message = "User not found." });
        }
    }
}
