using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageAPI.DBO;
using StorageAPI.DTO;
using StorageAPI.Errors;
using StorageAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace StorageAPI.Interfase.implementations
{
    public class GetUserInfoService : IGetUserInfo
    {
        private readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserInfoService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserInfoDTO> GetUserInfoFromToken()
        {
            // Get the Authorization header from the HttpContext
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer"))
            {
                throw new ToException("Unauthorized");
            }

            // Extract the token from the header
            string token = authHeader.Substring("Bearer \\".Length);
            token = token.Remove(token.Length-2);

            // Decode the token to retrieve the claims, including the username
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenParsed = tokenHandler.ReadJwtToken(token);
            // Get the username from the token's claims
            var usernameClaim = tokenParsed.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);
            if (usernameClaim == null)
            {
                throw new ToException("Invalid token");
            }
            var username = usernameClaim.Value;

            var exitUser = _context.Users.ToList().FirstOrDefault(x => x.Username == username);
            if (exitUser == null)
            {
                throw new ToException("User Does not Exist");
            }

            var userInfo = _mapper.Map<UserInfoDTO>(exitUser);
            return userInfo;
        }
    }
}