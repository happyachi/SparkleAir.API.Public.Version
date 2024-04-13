﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class MileageDetail
{
    public int Id { get; set; }

    public int MermberIsd { get; set; }

    public int TotalMile { get; set; }

    public int OriginalMile { get; set; }

    public int ChangeMile { get; set; }

    public int FinalMile { get; set; }

    public DateTime MileValidity { get; set; }

    public string MileReason { get; set; }

    public string OrderNumber { get; set; }

    public DateTime ChangeTime { get; set; }

    public virtual Member MermberIsdNavigation { get; set; }

    public virtual ICollection<MileApply> MileApplyChoses { get; set; } = new List<MileApply>();

    public virtual ICollection<MileApply> MileApplyEvents { get; set; } = new List<MileApply>();
}