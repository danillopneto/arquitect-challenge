namespace ArquitectChallenge.Domain.Events
{
    public class GroupEventByHour
    {
        public string Tag { get; set; }

        public int[] EventsByHour { get; set; }
    }
}