﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class TransferRefund
{
    public int Id { get; set; }

    public int TransferImethodsId { get; set; }

    public DateTime RefundDate { get; set; }

    public decimal RefundtAmount { get; set; }

    public int TransferPaymentsId { get; set; }

    public virtual ICollection<TicketCancel> TicketCancels { get; set; } = new List<TicketCancel>();

    public virtual TransferMethod TransferImethods { get; set; }

    public virtual TransferPayment TransferPayments { get; set; }
}