using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.MessageAndChats
{
    public class MessageConnectDto
    {
        public int MemberId { get; set; }

        public string Message { get; set; }

        public string ConnectionId { get; set; }
    }
}
