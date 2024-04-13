using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Members;
using SparkleAir.Infa.Utility.Exts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.Members
{
    public class MemberFunc
    {
        public static Func<Member, MemberEntity> MemberToEntityFunc = (member) =>
        {
            return member.MemberToEntity();
        };

        public static Func<Member, MemberGetCriteria, bool> MemberGetFunc = (member, criteria) =>
        {
            if (member.Account.Contains(criteria.Account)) return true;

            return false;
        };


        public static Func<Member, MemberSearchCriteria, bool> MemberSearchFunc = (member, criteria) =>
        {
            if (criteria == null) return true;

            if (member.Account.Contains(criteria.Account)) return true;

            return false;
        };
    }
}
