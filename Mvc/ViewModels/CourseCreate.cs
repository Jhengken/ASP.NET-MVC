using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mvc.ValidationAttribute;

#nullable disable

namespace Mvc.ViewModels;

public class CourseCreate : IValidatableObject
{
    [Required]
    [BindRequired]
    [判斷有無重複的課程名稱]
    public string Title { get; set; }

    public string Title2 { get; set; }

    [Required]
    [BindRequired]
    [Range(1, 10, ErrorMessage = "超出1-10範圍", MaximumIsExclusive = false)]
    public int? Credits { get; set; }
    public DateTime StartDate { get; set; }

    // 跨多個欄位再用的
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Title == "AAA" && Credits < 5)
        {
            yield return new ValidationResult("課程評分過低", [nameof(Title)]);
        }
    }
}