using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.DBO;
using StorageAPI.DTO;
using StorageAPI.Interfase;
using StorageAPI.Models;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace StorageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class LoginPageController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginPageController(ILogin login)
        {
            _login = login;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto)
        {
            return Ok(await _login.LoginUser(loginDto));
        }
    }
}