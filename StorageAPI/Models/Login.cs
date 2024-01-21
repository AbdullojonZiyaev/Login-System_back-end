using Microsoft.AspNetCore.Authentication;

namespace StorageAPI.Models
{
    public class Login: GenericId
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
