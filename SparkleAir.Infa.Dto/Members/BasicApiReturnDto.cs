using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Members
{
    public class BasicApiReturnDto
    {
        public string Status { get; set; }
        public bool IsVerify { get; set; }
        public string? Token { get; set; }

        public string? Error { get; set; }
        public string? Url { get; set; }


    }
}
