namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

public class Cart
    {
        public List<ProductInCart> ProductsList { get; set; } = new List<ProductInCart>();
    }