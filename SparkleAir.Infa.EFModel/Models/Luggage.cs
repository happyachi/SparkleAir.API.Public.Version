﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class Luggage
{
    public int Id { get; set; }

    public int AirFlightManagementsId { get; set; }

    public int OriginalPrice { get; set; }

    public int BookPrice { get; set; }

    public virtual AirFlightManagement AirFlightManagements { get; set; }

    public virtual ICollection<LuggageOrder> LuggageOrders { get; set; } = new List<LuggageOrder>();
}