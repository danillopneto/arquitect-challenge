using ArquitectChallenge.Domain.Events;
using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Services.Events
{
    public interface IEventService : IBaseService
    {
        IList<GroupEventData> GetAllGroupedByTag();

        IList<EventByDate> GetEventsGroupedByHour();

        IList<EventData> GetNumericEvents();
    }
}