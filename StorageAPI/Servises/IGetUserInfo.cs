using Microsoft.AspNetCore.Mvc;
using StorageAPI.DTO;
using StorageAPI.Interfase.implementations;
using StorageAPI.Models;

namespace StorageAPI.Interfase
{
    public interface IGetUserInfo
    {
        public Task<UserInfoDTO> GetUserInfoFromToken();

    }
}
