using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorageAPI.DBO;
using StorageAPI.DTO;
using StorageAPI.Interfase;
using StorageAPI.Interfase.implementations;
using StorageAPI.Models;
using System.Security.Cryptography;

namespace StorageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterPageController : ControllerBase
    {

        private readonly ILogger<RegisterPageController> _logger;
        private readonly IRegister _register;
        public RegisterPageController(ILogger<RegisterPageController> logger, IRegister register)
        {
            _logger = logger;
            _register = register;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterDTO registerDTO)
        {
            return Ok(await _register.RegisterUser(registerDTO));
        }
        
    }
}