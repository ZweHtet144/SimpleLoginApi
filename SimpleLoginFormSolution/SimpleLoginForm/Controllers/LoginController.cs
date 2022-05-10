using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleLoginForm.DTO;
using SimpleLoginForm.Helper;
using SimpleLoginForm.Models;
using SimpleLoginForm.Resources;
using SimpleLoginForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLoginForm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private LoginServices loginServices;
        private CustomHelper customHelper;
        public LoginController(LoginServices loginServices, CustomHelper customHelper)
        {
            this.loginServices = loginServices;
            this.customHelper = customHelper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("api/Login")]
        [HttpPost]
        public Task<LoginResponseModel> Login(BaseRequestModel request)
        {
            LoginDTO model = new LoginDTO();
            LoginResponseModel responseModel = new LoginResponseModel();
            model = JsonConvert.DeserializeObject<LoginDTO>(request.JsonStringRequest);

            bool isLoginSuccess = loginServices.Login(model);

            if (!isLoginSuccess)
            {
                responseModel.RespCode = ResponseCodeResource.E0001;
                responseModel.RespDesc = MessageResource.E0001;
            }
            else
            {
                //generate Encrypted key
                var generateKey = customHelper.GenerateKeyForToken(model.UserId, model.Password);
                string token = JWTManager.GenerateToken(generateKey);
                responseModel.RespCode = ResponseCodeResource.I0000;
                responseModel.RespDesc = MessageResource.I0000;
                responseModel.Token = token;
            }
            return Task.FromResult(responseModel);
        }
        [HttpPost]
        [Route("api/GetUserList")]
        public Task<UserModel> GetUserList(BaseRequestModel request)
        {
            UserModel responseModel = new UserModel();
            UserRequestModel requestModel = new UserRequestModel();
            requestModel = JsonConvert.DeserializeObject<UserRequestModel>(request.JsonStringRequest);
            string IsValid = JWTManager.ValidateToken(requestModel.Token);
            if (string.IsNullOrEmpty(IsValid))
            {
                responseModel.RespCode = ResponseCodeResource.E0002;
                responseModel.RespDesc = MessageResource.E0002;
                return Task.FromResult(responseModel);
            }
            responseModel = loginServices.GetUserList();
            if (responseModel != null)
            {
                responseModel.RespCode = ResponseCodeResource.I0000;
                responseModel.RespDesc = MessageResource.I0000;
                return Task.FromResult(responseModel);
            }
            responseModel.RespCode = ResponseCodeResource.E0001;
            responseModel.RespDesc = MessageResource.E0001;
            return Task.FromResult(responseModel);
        }
    }
}
