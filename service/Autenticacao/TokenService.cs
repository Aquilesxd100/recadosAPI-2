using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace recados_api
{
    public class TokenService{

        public static string GenerateToken(string Id){
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("jwtSecret") ?? Env.jwt_secret);
            var tokenConfig = new SecurityTokenDescriptor{
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", Id),
                }),
                Expires = DateTime.UtcNow.AddDays(7), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandle = new JwtSecurityTokenHandler();
            var token = tokenHandle.CreateToken(tokenConfig);
            var tokenString = tokenHandle.WriteToken(token);
            return tokenString;
        }
    }
}