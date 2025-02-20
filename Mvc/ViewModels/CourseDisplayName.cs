#nullable disable

using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Models;

[ModelMetadataType<CourseMetaData>]
public partial class Course
{
}

public class CourseMetaData
{
    public int CourseId { get; set; }
    [DisplayName("課程主題")] public string Title { get; set; }

    [DisplayName("課程評分A")] public int? Credits { get; set; }
}