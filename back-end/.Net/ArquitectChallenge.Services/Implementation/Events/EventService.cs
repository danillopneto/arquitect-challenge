using ArquitectChallenge.Interfaces.Repository.Events;
using ArquitectChallenge.Interfaces.Services.Events;

namespace ArquitectChallenge.Services.Implementation.Events
{
    public class EventService : BaseService, IEventService
    {
        public EventService(IEventRepository eventRepository) : base(eventRepository)
        {
        }
    }
}