using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Repository
{
    public interface IProductRepository
    {

        public Task<List<Product>> GetAll();
        public Task<Product?> GetById(int id);
        public Task<int> Insert(Product product);
        public Task<int> Update(int id, Product product);

        public Task<bool> Exists(string? name);
        public Task<int> DisableAsync(int id);
    }
}
