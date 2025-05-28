using System.ComponentModel.DataAnnotations;

namespace Condorcet.B2.AspnetCore.MVC.Application.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}