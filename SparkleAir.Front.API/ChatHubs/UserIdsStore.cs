namespace SparkleAir.Front.API.ChatHubs
{
    public static class UserIdsStore
    {
        public static HashSet<string> Ids = new HashSet<string>();

        public static Dictionary<int, string> MemberIdAndConnectionId = new Dictionary<int, string>();
    }
}
