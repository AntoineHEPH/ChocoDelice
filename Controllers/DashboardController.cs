using System;
using System.Linq;
using System.Threading.Tasks;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
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

        public DashboardController(IUserRepository userRepository,
                                   IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Récupérer tous les utilisateurs et produits
            var users = await _userRepository.GetAll();
            var products = await _productRepository.GetAll();

            // Construire le ViewModel
            var model = new DashboardViewModel
            {
                UserCount = users.Count(),
                ProductCount = products.Count(),
                OrderCount = 0, // TODO: remplacer par le nombre réel de commandes
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
                        Name = p.Name,
                        CreatedAt = DateTime.Now // TODO: remplacer par la date de création réelle du produit
                    })
            };

            return View(model);
        }
    }
}
