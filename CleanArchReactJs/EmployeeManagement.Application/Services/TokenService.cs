using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),

            new(ClaimTypes.Role, (user.UserRoles != null && user.UserRoles.Count>0) ? user.UserRoles.FirstOrDefault().Role.Name.ToString():"")

            
        };

            if (user.UserRoles!=null)
            {
                foreach (var userrole in user.UserRoles )
                {
                    if (userrole.Role != null)
                    {
                        foreach (var rolePermissions in userrole.Role.RolePermissions)
                        {
                            if (rolePermissions != null && rolePermissions.Permission !=null)
                            {
                                int len = rolePermissions.Permission.Name.IndexOf('.');
                                if (len>0)
                                {
                                    claims.Add(new Claim(rolePermissions.Permission.Name.Substring(0, len), rolePermissions.Permission.Name));
                                }

                            }
                        }
                    }
                }
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),

                Expires = DateTime.UtcNow.AddDays(
                    _jwtSettings.RefreshTokenExpirationDays),

                Created = DateTime.UtcNow,

                Revoked =null
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(
            string accessToken)
        {
            var tokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateAudience = true,

                    ValidateIssuer = true,

                    ValidateIssuerSigningKey = true,

                    ValidateLifetime = false,

                    ValidIssuer = _jwtSettings.Issuer,

                    ValidAudience = _jwtSettings.Audience,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_jwtSettings.SecretKey))
                };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(
                accessToken,
                tokenValidationParameters,
                out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }

        public bool ValidateRefreshToken(
            User user,
            string refreshToken)
        {
            if (user.RefreshToken == null)
                return false;

            if (user.RefreshToken.Token != refreshToken)
                return false;

            if (user.RefreshToken.Revoked != null)
                return false;

            if (user.RefreshToken.Expires <= DateTime.UtcNow)
                return false;

            return true;
        }
    }
}
