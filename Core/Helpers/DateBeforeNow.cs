using System.ComponentModel.DataAnnotations;

namespace Core;

public class DateBeforeNow : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || value is DateTime date && date < DateTime.Now) return ValidationResult.Success;

        return new ValidationResult("Date must be in the past.");
    }
}