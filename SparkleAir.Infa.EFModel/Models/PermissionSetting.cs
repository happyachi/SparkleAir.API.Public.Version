﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class PermissionSetting
{
    public int Id { get; set; }

    public int PermissionGroupsId { get; set; }

    public int PermissionPageInfoId { get; set; }

    public bool ViewPermission { get; set; }

    public bool EditPermission { get; set; }

    public bool CreatePermission { get; set; }

    public bool DeletePermission { get; set; }

    public virtual PermissionGroup PermissionGroups { get; set; }

    public virtual PermissionPageInfo PermissionPageInfo { get; set; }
}