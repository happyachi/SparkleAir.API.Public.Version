﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class AirCabinSeat
{
    public int Id { get; set; }

    public int AirTypeId { get; set; }

    public int AirCabinId { get; set; }

    public int? SeatNum { get; set; }

    public virtual AirCabin AirCabin { get; set; }

    public virtual AirType AirType { get; set; }
}