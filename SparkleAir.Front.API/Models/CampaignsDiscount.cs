﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class CampaignsDiscount
{
    public int Id { get; set; }

    public int CampaignId { get; set; }

    public string Name { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public DateTime DateCreated { get; set; }

    public string Status { get; set; }

    public decimal DiscountValue { get; set; }

    public decimal Value { get; set; }

    public decimal? BundleSkus { get; set; }

    public string MemberCriteria { get; set; }

    public string TfitemsCriteria { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public virtual Campaign Campaign { get; set; }

    public virtual ICollection<CampaignsDiscountMember> CampaignsDiscountMembers { get; set; } = new List<CampaignsDiscountMember>();

    public virtual ICollection<CampaignsDiscountStatusNotification> CampaignsDiscountStatusNotifications { get; set; } = new List<CampaignsDiscountStatusNotification>();

    public virtual ICollection<CampaignsDiscountTfitem> CampaignsDiscountTfitems { get; set; } = new List<CampaignsDiscountTfitem>();

    public virtual ICollection<CampaignsTfdiscountUsageHistory> CampaignsTfdiscountUsageHistories { get; set; } = new List<CampaignsTfdiscountUsageHistory>();
}