using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Exts.Models
{
    public static class MemberExts
    {
        public static MemberEntity MemberToEntity(this Member member)
        {
            MemberEntity memberEntity = new MemberEntity
            {
                Id = member.Id,
                MemberClassId = member.MemberClassId,
                MemberClassName = member.MemberClass.Name,
                CountryId = member.CountryId,
                CountryName = member.Country.ChineseName,
                Account = member.Account,
                Password = member.Password,
                ChineseLastName = member.ChineseLastName,
                ChineseFirstName = member.ChineseFirstName,
                EnglishLastName = member.EnglishLastName,
                EnglishFirstName = member.EnglishFirstName,
                Gender = member.Gender,
                DateOfBirth = new DateTime( member.DateOfBirth.Year, member.DateOfBirth.Month, member.DateOfBirth.Day),
                Phone = member.Phone,
                Email = member.Email,
                TotalMileage = member.TotalMileage,
                PassportNumber = member.PassportNumber,
                PassportExpiryDate = new DateTime(member.PassportExpiryDate.Year, member.PassportExpiryDate.Month, member.PassportExpiryDate.Day),
                RegistrationTime = member.RegistrationTime,
                LastPasswordChangeTime = member.LastPasswordChangeTime,
                IsAllow = member.IsAllow,
                ConfirmCode = member.ConfirmCode,
                PasswordSalt = member.PasswordSalt,
                GoogleId = member.GoogleId,
                LineId = member.LineId,
            };

            return memberEntity;
        }

        public static bool MemberSearchExts(this Member member, MemberSearchCriteria criteria)
        {
            if (member.Account.Contains(criteria.Account)) return true;

            return false;
        }

        public static IQueryable<Member> MemberSearchExt(this IQueryable<Member> members, MemberSearchCriteria criteria)
        {
            if (criteria == null) return members;


            // todo criteria篩選
            return members;
        }

        public static Member MemberUpdateExt(this Member member, MemberEntity entity)
        {
            return member;
        }
    }
}
