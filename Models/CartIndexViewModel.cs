namespace Condorcet.B2.AspnetCore.MVC.Application.Models;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

public class CartIndexViewModel
{
    public List<CartItem> Products { get; set; }

    public decimal Total => Products.Sum(p => p.Product.Prix * p.Quantity);
    public bool EstVide => !Products.Any();
}
