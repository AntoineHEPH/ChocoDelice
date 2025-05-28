using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;
using Condorcet.B2.AspnetCore.MVC.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Condorcet.B2.AspnetCore.MVC.Application.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;

        public ProductsController(IProductRepository productRepository, IUserRepository userRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }
        
        private Task<User?> GetCurrentUser()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return null;

            return _userRepository.GetByUsernameAsync(username);
        }
        

        // GET: ProjectsController
        public async Task<ActionResult> Index()
        {
            var products = await _productRepository.GetAll();
            return View(new ProductIndexViewModel()
            {
                Products = products
            });
        }

        [Authorize(Policy = "CreateProductPolicy")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _productRepository.Insert(new Product
            {
                Name = model.Name,
                Description = model.Description,
                Type = (int)model.Type,
                Prix = model.Prix,
                IsActive = true
            });
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return NotFound();
            return View(new ProductFormViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Type = (ProductType)product.Type,
                Prix = product.Prix
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _productRepository.Update(model.Id.Value, new Product
            {
                Name = model.Name,
                Description = model.Description,
                Type = (int)model.Type,
                Prix = model.Prix,
            });
            return RedirectToAction(nameof(Index));
        }
        
        [Authorize(Policy = "AdminUserAccess")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var user = await GetCurrentUser();
            if (user == null)
                return RedirectToAction("Login", "Account");

            await _cartRepository.Add(user.Id, productId, 1);
            return RedirectToAction(nameof(Index));
        }

        
        
        
        public async Task<IActionResult> Cart()
        {
            var user = await GetCurrentUser();

            if (user == null || user.Cart == null)
            {
                return View(new List<ProductInCart>()); // panier vide
            }

            return View(user.Cart.ProductsList);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Disable(int id)
        {
            var rowsAffected = await _productRepository.DisableAsync(id);
            if (rowsAffected > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        
        



        
        

    }
}