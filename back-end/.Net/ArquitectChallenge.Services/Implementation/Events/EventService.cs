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

        public IList<GroupEventByTag> GetAllGroupedByTag()
        {
            return _repository.GetAllGroupedByTag();
        }

        public IList<GroupEventByHour> GetEventsGroupedByHour(DateTime date)
        {
            return _repository.GetEventsGroupedByHour(date);
        }

        public IList<EventData> GetNewestEvents(int lastEventId)
        {
            return _repository.GetNewestEvents(lastEventId);
        }

        public IList<EventData> GetNumericEvents()
        {
            return _repository.GetNumericEvents();
        }

        public IList<NumericEventsData> GetNumericEventsData(DateTime date)
        {
            var groupped = _repository.GetNumericEventsGroupped(date);
            return groupped.GetNumericEventsData();
        }

        public override T Save<T>(T model)
        {
            /* Preparing object to be saved. */
            var modelPrepared = UtilExtensions.ConvertTo<EventData>(model);
            modelPrepared.PrepareModel();

            /* Casting back to T before saving. */
            return base.Save(modelPrepared.ConvertTo<T>());
        }
    }
}