﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SparkleAir.Front.API.Models;

public partial class CompanyStaffLoginLog
{
    public int Id { get; set; }

    public int CompanyStaffId { get; set; }

    public DateTime Logintime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public string Ipaddress { get; set; }

    public bool LoginStatus { get; set; }

    public virtual CompanyStaff CompanyStaff { get; set; }
}