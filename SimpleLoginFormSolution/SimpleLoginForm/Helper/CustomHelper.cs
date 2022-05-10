using Microsoft.Extensions.Configuration;
using SimpleLoginForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLoginForm.Helper
{
    public class CustomHelper
    {
        private EncryptionHelper encryptionHelper;
        private IConfiguration _configuration;
        public CustomHelper(EncryptionHelper encryptionHelper, IConfiguration _configuration)
        {
            this.encryptionHelper = encryptionHelper;
            this._configuration = _configuration;
        }
        public string GenerateKeyForToken(string UserId, string Password)
        {
            string secretKey = _configuration.GetValue<string>("MyKey");
            string generateKey = UserId + secretKey + Password;
            byte[] encryptKey = encryptionHelper.EncryptAesManaged(generateKey);
            string data = Convert.ToBase64String(encryptKey);
            return data;
        }
    }
}
