using SparkleAir.Infa.Dto.MessageAndChats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.MessageAndChats
{
    public interface IClientNotificationHub
    {
        //Task ClientReceiveNotification(MessageAndChatDto notification);

        //Task ClientReceiveMessage(string user, string message);
        Task SendAll(object message);
    }
}
