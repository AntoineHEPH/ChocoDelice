using System.ComponentModel.DataAnnotations;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

namespace Condorcet.B2.AspnetCore.MVC.Application.Models;

public class ProductIndexViewModel
{
    public List<Product> Products { get; set; }
}