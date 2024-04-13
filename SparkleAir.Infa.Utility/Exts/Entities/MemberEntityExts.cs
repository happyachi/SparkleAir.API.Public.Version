using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Exts.Entities
{
    public static class MemberEntityExts
    {
        public static MemberDto EntityToDto(this MemberEntity member)
        {
            MemberDto memberDto = new MemberDto
            {
                Id = member.Id,
                MemberClassId = member.MemberClassId,
                MemberClassName = member.MemberClassName,
                CountryId = member.CountryId,
                CountryName = member.CountryName,
                Account = member.Account,
                Password = member.Password,
                ChineseLastName = member.ChineseLastName,
                ChineseFirstName = member.ChineseFirstName,
                EnglishLastName = member.EnglishLastName,
                EnglishFirstName = member.EnglishFirstName,
                Gender = member.Gender,
                DateOfBirth = member.DateOfBirth,
                Phone = member.Phone,
                Email = member.Email,
                TotalMileage = member.TotalMileage,
                PassportNumber = member.PassportNumber,
                PassportExpiryDate = member.PassportExpiryDate,
                RegistrationTime = member.RegistrationTime,
                LastPasswordChangeTime = member.LastPasswordChangeTime,
                IsAllow = member.IsAllow,
                ConfirmCode = member.ConfirmCode,
                GoogleId = member.GoogleId,
                LineId = member.LineId,

            };

            return memberDto;
        }
    }
}
