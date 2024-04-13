using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.MessageAndChats
{
    public class NotificationHub : Hub<IClientNotificationHub>
    {
        //public Task SendMessage(string user, string message)
        //{
        //    return Clients.All.SendAsync("ClientReceiveMessage",user, message);
        //}

        
    }



    
}
