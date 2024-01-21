using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StorageAPI.DBO;
using StorageAPI.DTO;
using StorageAPI.Errors;
using StorageAPI.Models;
using System.Linq;

namespace StorageAPI.Interfase.implementations
{
    public class RegisterService: IRegister
    {
        private readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;
        public RegisterService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegisterDTO> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            var exitUser = await _context.Users.FirstOrDefaultAsync(x=>x.Username == registerDTO.Username);
            if (exitUser != null)
            {
                throw new ToException("User already exists");
            }
                var register = _mapper.Map<UserInfo>(registerDTO);
                _context.Users.Add(register);
                _context.SaveChanges();
                return registerDTO;
         
        }
    }
}
