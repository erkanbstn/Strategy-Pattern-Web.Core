using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using Strategy.Pattern.Core.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strategy.Pattern.Core.UI.Repositories
{
    public class ProductRepositoryFromSqlServer : IProductRepository
    {
        private readonly AppIdentityDbContext _context;

        public ProductRepositoryFromSqlServer(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Product product)
        {
            _context.Entry(product).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _context.Products.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Save(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            _context.Entry(product).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
