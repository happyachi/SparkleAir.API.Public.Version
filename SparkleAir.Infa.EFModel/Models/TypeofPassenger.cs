﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class TypeofPassenger
{
    public int Id { get; set; }

    public string PassengerType { get; set; }

    public int MinAge { get; set; }

    public int MaxAge { get; set; }

    public decimal PricePercent { get; set; }

    public virtual ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
}