﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class Tfcategory
{
    public int Id { get; set; }

    public string Category { get; set; }

    public virtual ICollection<Tfitem> Tfitems { get; set; } = new List<Tfitem>();
}