using System.ComponentModel.DataAnnotations;

namespace Condorcet.B2.AspnetCore.MVC.Application.Models;

public enum ProductType
{
    [Display(Name = "Bonbon")]
    Bonbon = 1,
    
    [Display(Name = "Chocolat")]
    Chocolat = 2
}