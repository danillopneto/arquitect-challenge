namespace ArquitectChallenge.Domain.Events
{
    public class EventData : BaseObject
    {
        public double TimeStamp { get; set; }

        public string Tag { get; set; }

        public string Valor { get; set; }
    }
}