﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class AirTakeOffStatus
{
    public int Id { get; set; }

    public int AirFlightId { get; set; }

    public DateTime ActualDepartureTime { get; set; }

    public DateTime ActualArrivalTime { get; set; }

    public int AirFlightStatusId { get; set; }

    public virtual AirFlight AirFlight { get; set; }

    public virtual AirFlightStatus AirFlightStatus { get; set; }
}