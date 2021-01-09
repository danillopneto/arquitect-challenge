using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Interfaces.Repository.Events;
using System.Collections.Generic;
using System.Linq;

namespace ArquitectChallenge.Services.Repository.Events
{
    public class EventRepository : BaseRepository<EventData>, IEventRepository
    {
        public EventRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public override T GetById<T>(int id)
        {
            return ConvertTo<T>(_dataContext.Events.Where(x => x.Id == id).FirstOrDefault());
        }

        public override IList<T> GetList<T>()
        {
            return _dataContext.Events.ToList().Select(x => ConvertTo<T>(x)).ToList();
        }

        protected override void UpdateItem(EventData currentItem, EventData updatedItem)
        {
            currentItem.Tag = updatedItem.Tag;
            currentItem.TimeStamp = updatedItem.TimeStamp;
            currentItem.Valor = updatedItem.Valor;
        }
    }
}