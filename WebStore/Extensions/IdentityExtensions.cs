using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Extensions
{
    public static class IdentityExtensions
    {
        public static string UserId(this ClaimsPrincipal user)
        {
            return user.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
        
        public static bool IsAuthorized(this ClaimsPrincipal user)
        {
            return user?.Identity.IsAuthenticated ?? false;
        }
        
        public static Guid UserGuidId(this ClaimsPrincipal user)
        {
            var stringId = user.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            return new Guid(stringId);
        }
        
        public static IEnumerable<string> UserRole(this ClaimsPrincipal user)
        {
            var role = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            return role;
        }
        
        public static string UserEmail(this ClaimsPrincipal user)
        {
            var role = user.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value).FirstOrDefault();
            return role;
        }
        
        
        public static string GenerateJWT(IEnumerable<Claim> claims, string signingKey, string issuer, int expiresInDays)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var singingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var expires = DateTime.Now.AddDays(expiresInDays);
            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                null,
                expires,
                singingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}