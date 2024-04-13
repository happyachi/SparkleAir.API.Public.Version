using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SparkleAir.Infa.Dto.MessageAndChats;

namespace SparkleAir.Front.API.ChatHubs
{
    public class ChatHub : Hub<IChatClient>
    {
        ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger, CommonService common)
        {
            _logger = logger;
            _common = common;
        }
        readonly CommonService _common;


        /// <summary>
        /// 客户端连接服务端
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;
            // 添加用户ID
            UserIdsStore.Ids.Add(id);
            _logger.LogInformation($"Client ConnectionId=> [[{id}]] Already Connection Server！");

             Clients.Caller.SendConnectionId(id);

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 客户端断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.ConnectionId;
            // 删除用户ID
            UserIdsStore.Ids.Remove(id);
            _logger.LogInformation($"Client ConnectionId=> [[{id}]] Already Close Connection Server!");
            return base.OnDisconnectedAsync(exception);
        }
        /**
         * 测试 
         * */
        /// <summary>
        /// 给所有客户端发送消息
        /// </summary>
        /// <returns></returns>
        public async Task SendMessage(string data)
        {
            var result = JsonConvert.DeserializeObject<MessageConnectDto>(data);

            string value;
            // 使用TryGetValue方法获取值
            if (UserIdsStore.MemberIdAndConnectionId.TryGetValue(result.MemberId, out value))
            {
                UserIdsStore.MemberIdAndConnectionId[result.MemberId] = result.ConnectionId;
            }
            else
            {
                UserIdsStore.MemberIdAndConnectionId.Add(result.MemberId, result.ConnectionId);
            }


            Console.WriteLine("Have one Data!");
           // await Clients.All.SendAll(_common.SendAll(data));
            await Clients.Caller.SendAll(_common.SendCaller());

        }
    }
}
