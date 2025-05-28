using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Repository
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order); // <- pour enregistrer une commande
        Task<List<Order>> GetOrdersByUserAsync(int userId); // <- historique d’un client
        Task<List<Order>> GetAllOrdersAsync(); // <- historique complet (admin)
    }
}
