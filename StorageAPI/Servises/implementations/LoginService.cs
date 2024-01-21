using StorageAPI.DBO;
using StorageAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.DTO;
using StorageAPI.Errors;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace StorageAPI.Interfase.implementations
{
    public class LoginService: ILogin
    {
        private readonly ApplicationDbContext _context;
        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }
        public static string GenerateToken(string username, int expireMinutes = 1)
        {
            var Secret = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(Secret);
            }
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username)
        }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
        public static string GenerateRefreshToken(string username, int expireMinutes = 2)
        {
            var Secret = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(Secret);
            }
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username)
        }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
        public async  Task<LoginDto> LoginUser([FromBody] LoginDto loginDTO)
        {
            loginDTO.Token = GenerateToken(loginDTO.Username);
            loginDTO.RefreshToken = GenerateRefreshToken(loginDTO.Username);
            var exitUser = _context.Users.ToList().FirstOrDefault(x => x.Username == loginDTO.Username && x.Password == loginDTO.Password);
            if (exitUser != null)
            {
                return loginDTO;
            }
            else
            {
                throw new ToException("User Does not exists");
            }
        }
    }
}
