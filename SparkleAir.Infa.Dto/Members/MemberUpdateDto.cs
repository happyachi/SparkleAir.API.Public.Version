using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Members
{
    public class MemberUpdateDto
    {
        public string ChineseLastName { get; set; }

        public string ChineseFirstName { get; set; }

        public string EnglishLastName { get; set; }

        public string EnglishFirstName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string PassportNumber { get; set; }

        public DateTime PassportExpiryDate { get; set; }
        public string Token { get; set; }

    }
}
