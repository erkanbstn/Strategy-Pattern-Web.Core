using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Strategy.Pattern.Core.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strategy.Pattern.Core.UI.Repositories
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepositoryFromMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("BaseDb");
            _products = database.GetCollection<Product>("Products");
        }

        public async Task Delete(Product product)
        {
            await _products.DeleteOneAsync(x => x.Id == product.Id);

        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _products.Find(b => b.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _products.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task Save(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task Update(Product product)
        {
            await _products.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }
    }
}
