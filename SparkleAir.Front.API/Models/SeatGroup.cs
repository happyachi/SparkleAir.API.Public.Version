﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class SeatGroup
{
    public int Id { get; set; }

    public int AirTypeId { get; set; }

    public int SeatAreaId { get; set; }

    public string SeatNum { get; set; }

    public virtual AirType AirType { get; set; }

    public virtual SeatArea SeatArea { get; set; }
}