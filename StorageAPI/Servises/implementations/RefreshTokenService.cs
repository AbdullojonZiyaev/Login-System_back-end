using AutoMapper;
using StorageAPI.DBO;
using StorageAPI.Errors;
using StorageAPI.Interfase.implementations;
using StorageAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using StorageAPI.Interfase;

namespace StorageAPI.Servises.implementations
{
    public class RefreshTokenService : IRefreshToken
    {
        private readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefreshTokenService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

            public async     Task<Token> RefreshToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenParsed = tokenHandler.ReadJwtToken(token);
            // Get the username from the token's claims
            var usernameClaim = tokenParsed.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);
            if (usernameClaim == null)
            {
                throw new ToException("Invalid token");
            }
            var username = usernameClaim.Value;
            var newToken = LoginService.GenerateToken(username);
            var freshToken = new Token { token = newToken }; 
            return freshToken;
        }
    }
}
