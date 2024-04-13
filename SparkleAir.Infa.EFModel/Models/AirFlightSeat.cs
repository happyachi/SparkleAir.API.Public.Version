﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class AirFlightSeat
{
    public int Id { get; set; }

    public int AirFlightId { get; set; }

    public int AirCabinId { get; set; }

    public string SeatNum { get; set; }

    public bool IsSeated { get; set; }

    public virtual ICollection<AirBookSeat> AirBookSeats { get; set; } = new List<AirBookSeat>();

    public virtual AirCabin AirCabin { get; set; }

    public virtual AirFlight AirFlight { get; set; }

    public virtual ICollection<TicketInvoicingDetail> TicketInvoicingDetails { get; set; } = new List<TicketInvoicingDetail>();
}