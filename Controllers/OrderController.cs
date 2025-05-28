using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Infrastructure;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Condorcet.B2.AspnetCore.MVC.Application.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        private async Task<User?> GetCurrentUser()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return null;

            return await _userRepository.GetByUsernameAsync(username);
        }

        // 🔁 Création de la commande (depuis le panier)
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            Console.WriteLine("🟢 [DEBUG] OrderController.Create() APPELÉ");

            var user = await GetCurrentUser();
            if (user == null)
            {
                Console.WriteLine("🔴 [DEBUG] Utilisateur non connecté");
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _cartRepository.GetAll(user.Id);
            Console.WriteLine($"🟡 [DEBUG] {cartItems.Count} articles dans le panier");

            if (!cartItems.Any())
            {
                TempData["Error"] = "Votre panier est vide.";
                return RedirectToAction("Index", "Cart");
            }

            foreach (var item in cartItems)
            {
                var order = new Order
                {
                    UserId = user.Id,
                    Username = user.Username,
                    ProductId = item.Product.Id,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Prix,
                    OrderDate = DateTime.Now
                };

                Console.WriteLine($"📦 Enregistrement commande pour produit : {item.Product.Name}");

                await _orderRepository.AddAsync(order);
            }

            await _cartRepository.Clear(user.Id);
            Console.WriteLine("🧹 Panier vidé");
            Console.WriteLine($"🧾 Commande ajoutée pour : {user.Username}");

            TempData["Success"] = "Votre commande a été enregistrée avec succès ! 🎉";
            return RedirectToAction("Index", "Cart");
        }


        // 📄 Historique de l'utilisateur
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();
            if (user == null)
                return RedirectToAction("Login", "Account");

            var orders = await _orderRepository.GetOrdersByUserAsync(user.Id);
            return View(orders);
        }

        // 🔍 Historique complet (admin)
        public async Task<IActionResult> All()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            var commandes = orders
                .GroupBy(o => new
                {
                    o.UserId,
                    RoundedDate = o.OrderDate.ToString("yyyy-MM-dd HH:mm")
                })
                .Select(g => new CommandeViewModel
                {
                    UserId = g.First().UserId,
                    Username = g.First().Username,
                    OrderDate = g.First().OrderDate, // Affiche quand même la vraie date
                    Produits = g.ToList()
                })  
                .ToList();

            return View(commandes);
        }



    }
}
