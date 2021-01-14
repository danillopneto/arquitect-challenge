using ArquitectChallenge.Domain.Events;
using System;
using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Services.Events
{
    public interface IEventService : IBaseService
    {
        IList<GroupEventByTag> GetAllGroupedByTag();

        IList<GroupEventByHour> GetEventsGroupedByHour(DateTime date);

        IList<EventData> GetNewestEvents(int lastEventId);

        IList<EventData> GetNumericEvents();
    }
}