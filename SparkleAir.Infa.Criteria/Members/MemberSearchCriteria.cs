using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Criteria.Members
{
    public class MemberSearchCriteria
    {
        public int MemberClassId { get; set; }

        public string MemberClassName { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public string Account { get; set; }

        public string ChineseLastName { get; set; }

        public string ChineseFirstName { get; set; }

        public string EnglishLastName { get; set; }

        public string EnglishFirstName { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int TotalMileage { get; set; }


        public DateTime RegistrationTime { get; set; }

        public DateTime LastPasswordChangeTime { get; set; }

        public bool IsAllow { get; set; }

    }
}
