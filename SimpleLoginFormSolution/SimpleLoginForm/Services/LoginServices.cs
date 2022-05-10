using Microsoft.Extensions.Configuration;
using SimpleLoginForm.DTO;
using SimpleLoginForm.Models;
using SimpleLoginForm.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLoginForm.Services
{
    public class LoginServices : ConnectionService
    {
        public LoginServices() : base() { }

        public LoginServices(IConfiguration configuration) : base(configuration) { }
        public bool Login(LoginDTO requestModel)
        {
            try
            {
                using (SqlConnection con = this.GetConnection())
                {
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResource.Login;
                    con.Open();
                    cmd.Parameters.AddWithValue("@UserName", requestModel.UserName);
                    cmd.Parameters.AddWithValue("@Password", requestModel.Password);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (!rd.HasRows)
                    {
                        rd.Close();
                        con.Close();
                        return false;
                    }
                    else
                    {
                        rd.Close();
                        con.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetUserList()
        {
            try
            {
                List<UserDTO> UserList = new List<UserDTO>();
                using (SqlConnection con = this.GetConnection())
                {
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResource.GetUserList;

                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            UserDTO DTO = new UserDTO();
                            DTO.UserId = GetValue<string>(rd["UserId"]);
                            DTO.UserName = GetValue<string>(rd["UserName"]);
                            UserList.Add(DTO);
                        }
                        rd.Close();
                    }
                    con.Close();
                }
                UserModel model = new UserModel();
                model.UserList = UserList;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
