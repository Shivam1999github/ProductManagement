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
    public class RefreshTokenRepository
    : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?>
            GetByTokenAsync(string token)
        {
            return await _context.RefreshToken
                .Include(x => x.User)
                .FirstOrDefaultAsync(x =>
                    x.Token == token);
        }

        public async Task AddAsync(
            RefreshToken token)
        {
            await _context.RefreshToken
                .AddAsync(token);
        }

        public void Update(
            RefreshToken token)
        {
            _context.RefreshToken.Update(token);
        }
    }
}
