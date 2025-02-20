﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; }

    [UIHint(nameof(Credits))]
    public int? Credits { get; set; }

    public int? DepartmentId { get; set; }

    public string Column2 { get; set; }

    public string Slug { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual Department Department { get; set; }
}