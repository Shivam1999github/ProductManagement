using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> RegisterAsync(
            RegisterRequest request);

        Task<IEnumerable<UserResponse>> GetAllAsync();

        Task<UserResponse?> GetByIdAsync(int id);

        Task UpdateAsync(
            int id,
            UpdateUserRequest request);

        Task DeleteAsync(int id);
    }
}
