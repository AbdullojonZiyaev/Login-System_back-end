using Microsoft.AspNetCore.Mvc;
using StorageAPI.DTO;
using StorageAPI.Interfase.implementations;
using StorageAPI.Models;

namespace StorageAPI.Interfase
{
    public interface IRegister
    {
        Task<RegisterDTO> RegisterUser([FromBody] RegisterDTO registerDTO);
    }
}
