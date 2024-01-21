using Microsoft.AspNetCore.Mvc;
using StorageAPI.DTO;
using StorageAPI.Interfase.implementations;
using StorageAPI.Models;

namespace StorageAPI.Interfase;

public interface IRefreshToken
{
    public Task<Token> RefreshToken(string token);
}
