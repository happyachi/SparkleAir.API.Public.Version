﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class CompanyJob
{
    public int Id { get; set; }

    public int CompanyDepartmentId { get; set; }

    public string JobTitle { get; set; }

    public int Rank { get; set; }

    public virtual CompanyDepartment CompanyDepartment { get; set; }

    public virtual ICollection<CompanyStaff> CompanyStaffs { get; set; } = new List<CompanyStaff>();
}