using System.ComponentModel.DataAnnotations;
using Mvc.Models;
#nullable disable
namespace Mvc.ValidationAttribute;

public class 判斷有無重複的課程名稱Attribute : System.ComponentModel.DataAnnotations.ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var title = value as string;
        if (string.IsNullOrWhiteSpace(title))
        {
            return ValidationResult.Success;
        }

        var contosoUniversityContext = validationContext.GetService<ContosoUniversityContext>();
        var exist = contosoUniversityContext.Courses.Any(course => course.Title == title);
        if (exist)
        {
            return new ValidationResult("已重複");
        }

        return ValidationResult.Success;
    }
}