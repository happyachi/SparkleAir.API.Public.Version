﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Infa.EFModel.Models;

public partial class CampaignsDiscountStatusNotification
{
    public int Id { get; set; }

    public int CampaignsDiscountId { get; set; }

    public int MemberId { get; set; }

    public bool IsReadByEmail { get; set; }

    public bool IsReadByBell { get; set; }

    public DateTime? NotificationTime { get; set; }

    public DateTime? ReadTime { get; set; }

    public string NotiHeader { get; set; }

    public string NotiBody { get; set; }

    public string Url { get; set; }

    public int? CompanyStaffsId { get; set; }

    public virtual CampaignsDiscount CampaignsDiscount { get; set; }

    public virtual CompanyStaff CompanyStaffs { get; set; }

    public virtual Member Member { get; set; }
}