using Microsoft.AspNetCore.Mvc;
using StorageAPI.DTO;
using StorageAPI.Interfase.implementations;

namespace StorageAPI.Interfase
{
    public interface ILogin
    {
Task<LoginDto> LoginUser([FromBody] LoginDto loginDTO);
    }
}
