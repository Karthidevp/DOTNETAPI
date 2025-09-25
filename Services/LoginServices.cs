using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoginServices
    {
        private readonly string _connectionString;

        public LoginServices(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<LoginResponse> LoginUserAsync(LoginModel login)
        {
            using var conn = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@EmailId", login.EmailId);
            parameters.Add("@Password", login.Password);
            parameters.Add("@LoginstatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Role", dbType: DbType.String, size: 20, direction: ParameterDirection.Output);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("LoginUser", parameters, commandType: CommandType.StoredProcedure);

            // ✅ Map the output params to your response object
            return new LoginResponse
            {
                LoginstatusId = parameters.Get<int>("@LoginstatusId"),
                Role = parameters.Get<string>("@Role"),
                Id = parameters.Get<int>("@Id")
            };
        }
    }
}
