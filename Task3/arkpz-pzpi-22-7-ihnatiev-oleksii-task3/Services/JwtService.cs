using AirSense.Application.Interfaces.Services;
using AirSense.Application.Options;
using AirSense.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity; // For using UserManager
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirSense.Application.Services
{
    // Service for generating JWT tokens
    public class JwtService(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager) : IJwtService
    {
        // Generate JWT token for a given user
        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            // Get the roles assigned to the user
            var roles = await userManager.GetRolesAsync(user);
            // Create a list of claims for the user's roles
            var roleClaims = roles.Select(role => new Claim("role", role));

            // Base claims for the token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // User's username as subject
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier for the token
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID
                new Claim(JwtRegisteredClaimNames.Email, user.Email) // User's email
            }
            .Concat(roleClaims); // Append the role claims to the base claims

            // Create a symmetric security key for signing the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
            // Set the signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer, // Token issuer
                audience: jwtOptions.Value.Audience, // Token audience
                claims: claims, // Claims for the token
                expires: DateTime.Now.AddDays(jwtOptions.Value.ExpireDays), // Token expiration time
                signingCredentials: creds); // Signing credentials

            // Return the token as a string
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
