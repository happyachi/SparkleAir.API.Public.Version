namespace SparkleAir.Front.API.ChatHubs
{
    public class CommonService
    {
        internal object SendAll(string data)
        {
            return $"Hello {new Random().Next(0, 100)} {data} ";
        }

        internal object SendCaller() => "Send Successful!";
    }
}
