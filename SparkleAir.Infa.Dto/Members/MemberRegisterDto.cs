using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Members
{
    public class MemberRegisterDto
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string? ChineseLastName { get; set; }

        public string? ChineseFirstName { get; set; }

        public string EnglishLastName { get; set; }

        public string EnglishFirstName { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string PassportNumber { get; set; }

        public DateTime PassportExpiryDate { get; set; }

    }
}
