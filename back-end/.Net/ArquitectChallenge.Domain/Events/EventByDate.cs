using System;

namespace ArquitectChallenge.Domain.Events
{
    public class EventByDate
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}