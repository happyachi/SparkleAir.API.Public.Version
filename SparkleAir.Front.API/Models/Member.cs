﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class Member
{
    public int Id { get; set; }

    public int MemberClassId { get; set; }

    public int CountryId { get; set; }

    public string Account { get; set; }

    public string Password { get; set; }

    public string ChineseLastName { get; set; }

    public string ChineseFirstName { get; set; }

    public string EnglishLastName { get; set; }

    public string EnglishFirstName { get; set; }

    public bool Gender { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int TotalMileage { get; set; }

    public string PassportNumber { get; set; }

    public DateOnly PassportExpiryDate { get; set; }

    public DateTime RegistrationTime { get; set; }

    public DateTime LastPasswordChangeTime { get; set; }

    public bool IsAllow { get; set; }

    public string ConfirmCode { get; set; }

    public string PasswordSalt { get; set; }

    public string GoogleId { get; set; }

    public string LineId { get; set; }

    public virtual ICollection<CampaignsCouponMember> CampaignsCouponMembers { get; set; } = new List<CampaignsCouponMember>();

    public virtual ICollection<CampaignsDiscountMember> CampaignsDiscountMembers { get; set; } = new List<CampaignsDiscountMember>();

    public virtual ICollection<CampaignsDiscountStatusNotification> CampaignsDiscountStatusNotifications { get; set; } = new List<CampaignsDiscountStatusNotification>();

    public virtual Country Country { get; set; }

    public virtual MemberClass MemberClass { get; set; }

    public virtual ICollection<MemberLoginLog> MemberLoginLogs { get; set; } = new List<MemberLoginLog>();

    public virtual ICollection<MessageBox> MessageBoxes { get; set; } = new List<MessageBox>();

    public virtual ICollection<MileOrder> MileOrders { get; set; } = new List<MileOrder>();

    public virtual ICollection<MileageDetail> MileageDetails { get; set; } = new List<MileageDetail>();

    public virtual ICollection<Tfreserf> Tfreserves { get; set; } = new List<Tfreserf>();

    public virtual ICollection<Tfwishlist> Tfwishlists { get; set; } = new List<Tfwishlist>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}