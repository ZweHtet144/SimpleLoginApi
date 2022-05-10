using SimpleLoginForm.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLoginForm.Models
{
    public class LoginResponseModel:BaseResponseModel
    {
        public string Token { get; set; }
    }
    public class UserModel:BaseResponseModel
    {
        public List<UserDTO> UserList { get; set; }
    }
    public class UserRequestModel
    {
        public string Token { get; set; }
    }
}
