using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Domain.Utilities;
using ArquitectChallenge.Interfaces.Repository.Events;
using ArquitectChallenge.Interfaces.Services.Events;
using System;
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

        public IList<EventByDate> GetEventsGroupedByHour()
        {
            return _repository.GetEventsGroupedByHour();
        }

        public IList<EventData> GetNumericEvents()
        {
            return _repository.GetNumericEvents();
        }

        public override T Save<T>(T model)
        {
            /* Preparing object to be saved. */
            var modelPrepared = UtilExtensions.ConvertTo<EventData>(model);
            modelPrepared.PrepareToSave();

            /* Casting back to T before saving. */
            return base.Save(modelPrepared.ConvertTo<T>());
        }
    }
}