using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLoginForm.Models
{
    public class BaseRequestModel
    {
        public string JsonStringRequest { get; set; }
    }
    public class BaseResponseModel
    {
        public string RespCode { get; set; }
        public string RespDesc { get; set; }
    }
}
