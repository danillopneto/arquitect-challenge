namespace ArquitectChallenge.Domain.Events
{
    public class GroupEventByHour
    {
        public int Count { get; set; }

        public bool IsRegion { get; set; }

        public string Tag { get; set; }

        public long Timestamp { get; set; }
    }
}