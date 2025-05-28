using System.ComponentModel.DataAnnotations;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;
using Condorcet.B2.AspnetCore.MVC.Application.Models.Validation;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condorcet.B2.AspnetCore.MVC.Application.Models;

public class ProductFormViewModel : IValidatableObject
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Le titre est obligatoire")]
    [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères")]
    public string Name { get; set; }
    

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Le type est obligatoire")]
    [Range(1, 2, ErrorMessage = "la priorité doit être entre 1 et 2")]
    public ProductType Type { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Le budget doit être positif")]
    public decimal Prix { get; set; }
    
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (Type == ProductType.Chocolat && Prix < 5)
        {
            results.Add(new ValidationResult(
                "Les chocolats doivent avoir un prix minimum de 5€",
                new[] { nameof(Type), nameof(Prix) }));
        }

        return results;
    }
}