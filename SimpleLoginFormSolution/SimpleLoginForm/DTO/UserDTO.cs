using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLoginForm.DTO
{
    public class LoginDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
    public class UserDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
