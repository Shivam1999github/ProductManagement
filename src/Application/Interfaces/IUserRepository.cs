using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(int id);

        Task<ApplicationUser?> GetByUserNameAsync(string userName);

        Task<ApplicationUser?> GetByEmailAsync(string email);

        Task<IEnumerable<ApplicationUser>> GetAllAsync();

        Task AddAsync(ApplicationUser user);

        void Update(ApplicationUser user);

        void Delete(ApplicationUser user);
    }
}
