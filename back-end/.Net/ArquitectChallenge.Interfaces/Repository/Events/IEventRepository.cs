using ArquitectChallenge.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArquitectChallenge.Interfaces.Repository.Events
{
    public interface IEventRepository : IBaseRepository
    {
        IList<GroupEventByTag> GetAllGroupedByTag();

        IList<GroupEventByHour> GetEventsGroupedByHour(DateTime date);

        IList<EventData> GetNewestEvents(int lastEventId);

        IList<EventData> GetNumericEvents();

        IList<IGrouping<string, EventData>> GetNumericEventsGroupped(DateTime date);
    }
}