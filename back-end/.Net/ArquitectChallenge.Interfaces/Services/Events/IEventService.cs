using ArquitectChallenge.Domain.Events;
using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Services.Events
{
    public interface IEventService : IBaseService
    {
        public IList<GroupEventData> GetAllGroupedByTag();
    }
}