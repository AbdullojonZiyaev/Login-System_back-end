using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.DTO;
using StorageAPI.Interfase;
using StorageAPI.Models;

namespace StorageAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetUserInfoController : ControllerBase
    {
        private readonly IGetUserInfo _userInfo;
        public GetUserInfoController(IGetUserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(await _userInfo.GetUserInfoFromToken());
        }
    }
}
