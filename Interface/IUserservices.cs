using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using service;

namespace Interface
{
    public class IUserservices
    {
        Task<IEnumerable<UserModels>> GetAllUsersAsync();
        Task<UserModels> GetUserByIdAsync(int id);

        Task<(bool Success, string Message)> CreateUserAsync(UserModels user);
        Task<(bool Success, string Message)> UpdateUserAsync(UserModels user);
        Task<(bool Success, string Message)> DeleteUserAsync(int id);
    }
}
