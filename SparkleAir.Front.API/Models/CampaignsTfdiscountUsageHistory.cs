﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class CampaignsTfdiscountUsageHistory
{
    public int Id { get; set; }

    public int CampaignsDiscountsId { get; set; }

    public int TfreservelistId { get; set; }

    public DateTime UsedDate { get; set; }

    public string Status { get; set; }

    public DateTime? DateCreated { get; set; }

    public int OriginalPrice { get; set; }

    public int DiscountAmount { get; set; }

    public int DiscountedPrice { get; set; }

    public virtual CampaignsDiscount CampaignsDiscounts { get; set; }

    public virtual Tfreservelist Tfreservelist { get; set; }
}