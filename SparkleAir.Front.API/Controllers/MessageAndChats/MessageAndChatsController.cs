using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using SparkleAir.BLL.Service.MessageAndChats;
using SparkleAir.Front.API.ChatHubs;
using SparkleAir.Front.API.Models;
using SparkleAir.Infa.Dto.MessageAndChats;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.MessageAndChats
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageAndChatsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly MessageAndChatsService _service;
        public MessageAndChatsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _service = new MessageAndChatsService(_appDbContext);
        }

        [HttpGet("getMessageByMemberId")]
        public List<MessageAndChatDto> GetMessageByMemberId(int memberId)
        {
            var messageList = _service.GetMessageByMemberId(memberId);
            return messageList;
        }

        //[HttpPost("createMessage")]
        //public void CreateMessage(MessageAndChatDto dto, [FromServices] IHubContext<ChatHub, IChatClient> hubContext)
        //{
        //    _service.CreateMessage(dto);

        //}

        [HttpPost("createMessage")]
        public async Task<IActionResult> CreateMessage(MessageAndChatDto dto, [FromServices] IHubContext<ChatHub, IChatClient> hubContext)
        {
            _service.CreateMessage(dto);
            var messageList = _service.GetMessageByMemberId((int)dto.MemberId);

            if(string.IsNullOrEmpty(dto.ConnectionId))
            {
                string value;
                // 使用TryGetValue方法获取值
                if (UserIdsStore.MemberIdAndConnectionId.TryGetValue((int)dto.MemberId, out value))
                {
                    await hubContext.Clients.Client(value).SendMessageList(messageList);
                }
            }
            else
            {
                string value;
                // 使用TryGetValue方法获取值
                if (UserIdsStore.MemberIdAndConnectionId.TryGetValue((int)dto.MemberId, out value))
                {
                    UserIdsStore.MemberIdAndConnectionId[(int)dto.MemberId] = dto.ConnectionId;
                }
                else
                {
                    UserIdsStore.MemberIdAndConnectionId.Add((int)dto.MemberId, dto.ConnectionId);
                }
                await hubContext.Clients.Client(dto.ConnectionId).SendMessageList(messageList);
            }

            string employeeId;
            UserIdsStore.MemberIdAndConnectionId.TryGetValue(0, out employeeId);
            if (employeeId != null)
            {
                await hubContext.Clients.Client(employeeId).SendMessageList(messageList);
            }

            return Ok(messageList);
        }

        

        [HttpGet("setEmployeeConnectionId")]
        public void SetEmployeeConnectionId(string connectionId)
        {
            string value;

            if (UserIdsStore.MemberIdAndConnectionId.TryGetValue(0, out value))
            {
                UserIdsStore.MemberIdAndConnectionId[0] = connectionId;
            }
            else
            {
                UserIdsStore.MemberIdAndConnectionId.Add(0, connectionId);
            }
        }

        [HttpGet("getAllMembers")]
        public List<MessageMemberInfoDto> GetAllMembers()
        {
            var memberList = _service.GetAllMembers();
            return memberList;
        }


        [HttpGet(Name = "SendMessage")]
        public async Task SendMessage(string data, [FromServices] IHubContext<ChatHub, IChatClient> hubContext)
        {
            await hubContext.Clients.All.SendAll(data);
        }

        /// <summary>
        /// 获取所有的用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUserIds", Name = "GetAllUserIds")]
        public string[] GetAllUserIds()
        {
            return UserIdsStore.Ids.ToArray();
        }

        /// <summary>
        /// 发送指定的消息给指定的客户端
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="data"></param>
        /// <param name="hubContext"></param>
        /// <returns></returns>
        [HttpGet("SendCustomUserMessage", Name = "SendCustomUserMessage")]
        public async Task<IActionResult> SendCustomUserMessage(string userid, string data, [FromServices] IHubContext<ChatHub, IChatClient> hubContext)
        {
            await hubContext.Clients.Client(userid).SendCustomUserMessage(data);
            return Ok("Send Successful!");
        }
    }
}
