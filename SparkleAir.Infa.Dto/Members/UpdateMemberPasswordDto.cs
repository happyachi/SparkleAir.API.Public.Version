using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Members
{
    public class UpdateMemberPasswordDto
    {
        public string PasswordOld { get; set; }

        public string PasswordNew { get; set; }

        public string PasswordConfirm { get; set; }

        public string Token { get; set; }

    }
}
