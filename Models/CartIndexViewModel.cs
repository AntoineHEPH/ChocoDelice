using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
namespace Condorcet.B2.AspnetCore.MVC.Application.Models;

public class CartIndexViewModel
{
    public List<CartItem> Products { get; set; }

    public decimal Total => Products.Sum(p => p.Product.Prix * p.Quantity);
    public bool EstVide => !Products.Any();
}