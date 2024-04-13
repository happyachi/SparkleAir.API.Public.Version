using SparkleAir.Infa.Dto.MessageAndChats;

namespace SparkleAir.Front.API.ChatHubs
{
    public interface IChatClient
    {
        Task SendAll(object message);

        Task SendCustomUserMessage(object message);

        Task SendMessageList(List<MessageAndChatDto> listDto);

        Task SendConnectionId(string connectionId);
    }
}
