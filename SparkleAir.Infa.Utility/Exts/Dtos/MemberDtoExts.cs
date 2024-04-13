using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Exts.Dtos
{
    public static class MemberDtoExts
    {
        public static MemberEntity DotToEntity(this MemberDto member)
        {
            MemberEntity memberEntity = new MemberEntity
            {
                Id = member.Id,
                MemberClassId = member.MemberClassId,
                CountryId = member.CountryId,
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
                ConfirmCode = member.ConfirmCode
            };

            return memberEntity;
        }
    }
}
