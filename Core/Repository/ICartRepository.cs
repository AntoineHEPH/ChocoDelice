using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;

public interface ICartRepository
{
    Task<List<CartItem>> GetAll(int userId);
    Task Add(int userId, int productId, int quantity);
    Task Remove(int userId, int productId);
    Task Clear(int userId);
    Task UpdateQuantity(int userId, int productId, int quantity);
}