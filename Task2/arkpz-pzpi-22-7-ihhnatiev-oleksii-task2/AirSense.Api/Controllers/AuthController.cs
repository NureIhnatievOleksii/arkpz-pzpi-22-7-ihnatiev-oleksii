﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using AirSense.Application.CQRS.Commands.Auth.Login;
using AirSense.Application.CQRS.Commands.Auth.Logout;
using AirSense.Application.CQRS.Commands.Auth.Register;
using AirSense.Application.CQRS.Commands.Auth;

namespace AirSense.Api.Controllers;

[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "User registered successfully." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login-with-google")]
    public async Task<IActionResult> LoginWithGoogle([FromBody] LoginWithGoogleCommand command,
    CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { result.Token, Message = "Login information added successfully." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login-with-github")]
    public async Task<IActionResult> LoginWithGitHub([FromQuery] LoginWithGitHubCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { result.Token, Message = "GitHub login successful." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { result.Token, Message = "User logged in successfully." });
        }

        return StatusCode(403, result.ErrorMessage);
    }

    [HttpPut("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "Logout successful." });
        }

        return BadRequest(result.ErrorMessage);
    }
}