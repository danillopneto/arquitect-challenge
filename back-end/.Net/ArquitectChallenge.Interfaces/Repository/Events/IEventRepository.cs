using ArquitectChallenge.Domain.Events;
using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Repository.Events
{
    public interface IEventRepository : IBaseRepository
    {
        IList<EventData> GetNumericEvents();

        IList<GroupEventData> GetAllGroupedByTag();
    }
}