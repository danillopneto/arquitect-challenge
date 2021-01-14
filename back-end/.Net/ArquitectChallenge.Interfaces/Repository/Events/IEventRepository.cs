using ArquitectChallenge.Domain.Events;
using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Repository.Events
{
    public interface IEventRepository : IBaseRepository
    {
        IList<GroupEventData> GetAllGroupedByTag();

        IList<EventByDate> GetEventsGroupedByHour();

        IList<EventData> GetNewestEvents(int lastEventId);

        IList<EventData> GetNumericEvents();
    }
}