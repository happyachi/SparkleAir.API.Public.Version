﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class Country
{
    public int Id { get; set; }

    public string ChineseName { get; set; }

    public string EnglishName { get; set; }

    public int? OrderBy { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<TicketInvoicingDetail> TicketInvoicingDetails { get; set; } = new List<TicketInvoicingDetail>();
}