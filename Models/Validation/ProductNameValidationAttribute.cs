using System.ComponentModel.DataAnnotations;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;

namespace Condorcet.B2.AspnetCore.MVC.Application.Models.Validation;

public class ProductNameValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Le nom est requis");
        }

        var currentModel = validationContext.ObjectInstance as ProductFormViewModel;
        
        var currentId = currentModel?.Id;
        
        var repository = validationContext.GetRequiredService<IProductRepository>();
        
        return ValidationResult.Success;
    }
}