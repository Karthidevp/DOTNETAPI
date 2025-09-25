using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public class UserServices: 
    {
        private readonly IDbConnection _dbConnection;

        public UserServices(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // ✅ Get all users
        public async Task<IEnumerable<UserModels>> GetAllUsersAsync()
        {
            var sql = "SELECT * FROM Users WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<UserModels>(sql);
        }

        // ✅ Get single user by Id
        public async Task<UserModels> GetUserByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE UserId = @UserId AND IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<UserModels>(sql, new { UserId = id });
        }

        // ✅ Create user using SP
        public async Task<(bool Success, string Message)> CreateUserAsync(UserModels user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "I");
            parameters.Add("@Name", user.Name);
            parameters.Add("@EmailId", user.EmailId);
            parameters.Add("@Address", user.Address);
            parameters.Add("@Password", user.Password);
            parameters.Add("@PhoneNo", user.PhoneNo);
            parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("USPInsertUpdateDeleteUser", parameters, commandType: CommandType.StoredProcedure);

            bool success = parameters.Get<bool>("@Success");
            string message = parameters.Get<string>("@Message");

            return (success, message);
        }

        // ✅ Update user using SP
        public async Task<(bool Success, string Message)> UpdateUserAsync(UserModels user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "U");
            parameters.Add("@UserId", user.UserId);
            parameters.Add("@Name", user.Name);
            parameters.Add("@EmailId", user.EmailId);
            parameters.Add("@Address", user.Address);
            parameters.Add("@Password", user.Password);
            parameters.Add("@PhoneNo", user.PhoneNo);
            parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("USPInsertUpdateDeleteUser", parameters, commandType: CommandType.StoredProcedure);

            bool success = parameters.Get<bool>("@Success");
            string message = parameters.Get<string>("@Message");

            return (success, message);
        }

        // ✅ Soft delete user using SP
        public async Task<(bool Success, string Message)> DeleteUserAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "D");
            parameters.Add("@UserId", id);
            parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("USPInsertUpdateDeleteUser", parameters, commandType: CommandType.StoredProcedure);

            bool success = parameters.Get<bool>("@Success");
            string message = parameters.Get<string>("@Message");

            return (success, message);
        }
    }
}
