#nullable disable
namespace Mvc.Models;

public partial class Course
{
    // 明確轉型
    // public static explicit operator CourseList(Course course)
    // {
    //     return new CourseList
    //     {
    //         CourseId = course.CourseId,
    //         Title = course.Title,
    //         Credits = course.Credits,
    //         Slug = course.Slug,
    //         Name = course.Department?.Name
    //     };
    // }
    
    // 隱含轉型
    // public static implicit operator CourseList(Course course)
    // {
    //     return new CourseList
    //     {
    //         CourseId = course.CourseId,
    //         Title = course.Title,
    //         Credits = course.Credits,
    //         Slug = course.Slug,
    //         Name = course.Department?.Name
    //     };
    // }
}