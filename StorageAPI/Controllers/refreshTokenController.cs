using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.DTO;
using StorageAPI.Interfase;

namespace StorageAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IRefreshToken _refreshToken;
        public RefreshTokenController(IRefreshToken refreshToken)
        {
            _refreshToken = refreshToken;
        }
        [HttpPost]
        public async Task<IActionResult> Post(string token)
        {
            return Ok(await _refreshToken.RefreshToken(token));
        }
    }
}
