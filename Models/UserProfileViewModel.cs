namespace Condorcet.B2.AspnetCore.MVC.Application.Models;

public class UserProfileViewModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Role { get; set; }
}