using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Interfaces.Repository.Events;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ArquitectChallenge.Services.Repository.Events
{
    public class EventRepository : BaseRepository<EventData>, IEventRepository
    {
        public EventRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IList<GroupEventData> GetAllGroupedByTag()
        {
            var groupped = _dataContext.Events.Where(x => !string.IsNullOrWhiteSpace(x.Tag))
                                .GroupBy(x => x.Tag)
                                .Select(x => new GroupEventData
                                {
                                    Tag = x.Key,
                                    Quantity = x.Count()
                                }).ToList();
            if (groupped.Count > 0)
            {
                groupped.AddRange(groupped.Where(x => !string.IsNullOrWhiteSpace(x.FirstTag()))
                            .GroupBy(x => x.FirstTag()).Select(x => new GroupEventData
                            {
                                Tag = x.Key,
                                Quantity = x.Sum(x => x.Quantity)
                            }).ToList());

                groupped.AddRange(groupped.Where(x => !string.IsNullOrWhiteSpace(x.SecondTag()))
                            .GroupBy(x => x.SecondTag()).Select(x => new GroupEventData
                            {
                                Tag = x.Key,
                                Quantity = x.Sum(x => x.Quantity)
                            }).ToList());

            }

            return groupped.OrderBy(x => x.Tag).ToList();
        }

        public override T GetById<T>(int id)
        {
            return ConvertTo<T>(_dataContext.Events.Where(x => x.Id == id).FirstOrDefault());
        }

        public override IList<T> GetList<T>()
        {
            return _dataContext.Events.AsNoTracking().ToList().Select(x => ConvertTo<T>(x)).ToList();
        }

        protected override void UpdateItem(EventData currentItem, EventData updatedItem)
        {
            currentItem.Tag = updatedItem.Tag;
            currentItem.TimeStamp = updatedItem.TimeStamp;
            currentItem.Valor = updatedItem.Valor;
        }
    }
}