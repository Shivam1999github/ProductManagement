using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.ApplicationUser
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _context.ApplicationUser
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser?> GetByUserNameAsync(
            string userName)
        {
            return await _context.ApplicationUser
                .FirstOrDefaultAsync(x =>
                    x.UserName == userName);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(
            string email)
        {
            return await _context.ApplicationUser
                .FirstOrDefaultAsync(x =>
                    x.Email == email);
        }

        public async Task AddAsync(
            ApplicationUser user)
        {
            await _context.ApplicationUser.AddAsync(user);
        }

        public void Update(
            ApplicationUser user)
        {
            _context.ApplicationUser.Update(user);
        }

        public void Delete(
            ApplicationUser user)
        {
            _context.ApplicationUser.Remove(user);
        }
    }
}
