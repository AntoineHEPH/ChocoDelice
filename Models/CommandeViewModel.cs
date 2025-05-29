using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

public class CommandeViewModel
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public List<Order> Produits { get; set; } = new();
}