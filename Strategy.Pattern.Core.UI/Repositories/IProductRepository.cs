using Strategy.Pattern.Core.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strategy.Pattern.Core.UI.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(string id);
        Task<List<Product>> GetAllByUserId(string userId);
        Task Save(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
