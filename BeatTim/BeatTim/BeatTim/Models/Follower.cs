namespace BeatTim.Models
{
    public class Follower
    {
        public int FollowerId { get; set; }
        public int UserId { get; set; }
        public Client User { get; set; }
        public int SubscriberId { get; set; }
        public Client Subscriber { get; set; }
    }
}