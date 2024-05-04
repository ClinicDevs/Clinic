using Clinic.Domain.Entities.Auth;          // User model ishlashi uchun 
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;   // Iconfiguration ishlashi uchun
using Microsoft.IdentityModel.Tokens;       // SymmetricSecurityKey, SigningCredentials, EpochTime ishlashi uchun
using System.Globalization;                 // CultureInfo ishlashi uchun    
using System.IdentityModel.Tokens.Jwt;      // JwtRegisteredClaimNames, JwtSecurityToken ishlashi uchun
using System.Security.Claims;               // Claim ishlashi uchun 
using System.Text;                          // Encoding ishlashi uchun
using System.Text.Json;                     // JsonSerializer ishlashi uchun

namespace Clinic.Application.UseCases.AuthService
{
    public class AuthService(IConfiguration config, UserManager<User> userManager) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _config = config;

        public async Task<string> GenerateToken(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Secret"]!));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            int expirePeriod = int.Parse(_config["JWTSettings:Expire"]!);
            var role = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat,EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),ClaimValueTypes.Integer64),
                new("UserName",user.UserName!),
                new(ClaimTypes.Name,user.Firsname!),
                new("role",role[0]),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWTSettings:ValidIssuer"],
                audience: _config["JWTSettings:ValidAudence"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirePeriod),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
