﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class PermissionPageInfo
{
    public int Id { get; set; }

    public string PageNumber { get; set; }

    public string PageName { get; set; }

    public string PageDescription { get; set; }

    public virtual ICollection<PermissionSetting> PermissionSettings { get; set; } = new List<PermissionSetting>();
}