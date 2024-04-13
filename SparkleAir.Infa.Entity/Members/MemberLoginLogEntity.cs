using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Members
{
    public class MemberLoginLogEntity
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public DateTime Logintime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public string IPAddress { get; set; }

        public bool LoginStatus { get; set; }
    }
}
