using System.Security.Cryptography.X509Certificates;

namespace StorageAPI.Models
{
    public class UserInfo: GenericId
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
    }
}
