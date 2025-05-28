namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

public class CartItem
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Product Product { get; set; }
}