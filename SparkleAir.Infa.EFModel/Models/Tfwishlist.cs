﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class Tfwishlist
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int TfitemsId { get; set; }

    public virtual Member Member { get; set; }

    public virtual Tfitem Tfitems { get; set; }
}