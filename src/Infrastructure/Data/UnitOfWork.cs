using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; }

        //public IItemRepository Items { get; }

        public IUserRepository Users { get; }

        public IRefreshTokenRepository RefreshTokens { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IProductRepository products,
            //IItemRepository items,
            IUserRepository users,
            IRefreshTokenRepository refreshTokens)
        {
            _context = context;

            Products = products;
            //Items = items;
            Users = users;
            RefreshTokens = refreshTokens;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }
    }
}
