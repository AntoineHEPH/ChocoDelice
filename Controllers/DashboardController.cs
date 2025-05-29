using Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;
using Condorcet.B2.AspnetCore.MVC.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Condorcet.B2.AspnetCore.MVC.Application.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public DashboardController(IUserRepository userRepository,
                                   IProductRepository productRepository,
                                   IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAll();
            var products = await _productRepository.GetAll();
            var orders = await _orderRepository.GetAllOrdersAsync();
            
            var model = new DashboardViewModel
            {
                UserCount = users.Count(),
                ProductCount = products.Count(),
                OrderCount = orders
                    .GroupBy(o => new { 
                        o.UserId, 
                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd HH:mm") 
                    })
                    .Count(),
                RecentUsers = users
                    .OrderByDescending(u => u.CreatedAt)
                    .Take(5)
                    .Select(u => new RecentUserDto
                    {
                        Username = u.Username,
                        CreatedAt = u.CreatedAt
                    }),
                RecentProducts = products
                    .Take(5)
                    .Select(p => new RecentProductDto
                    {
                        Name = p.Name
                    })
            };

            return View(model);
        }
    }
}