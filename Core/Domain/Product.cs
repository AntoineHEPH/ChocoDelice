namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

public class Product
{

    public int Id { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public required int Type { get; set; }
    public decimal Prix { get; set; }
    public bool IsActive { get; set; }

}