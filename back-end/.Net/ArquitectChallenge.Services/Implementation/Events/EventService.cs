using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Interfaces.Repository.Events;
using ArquitectChallenge.Interfaces.Services.Events;
using System.Collections.Generic;

namespace ArquitectChallenge.Services.Implementation.Events
{
    public class EventService : BaseService<IEventRepository>, IEventService
    {
        public EventService(IEventRepository eventRepository) : base(eventRepository)
        {
        }

        public IList<GroupEventData> GetAllGroupedByTag()
        {
            return _repository.GetAllGroupedByTag();
        }
    }
}