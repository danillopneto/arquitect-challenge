using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Domain.Utilities;
using ArquitectChallenge.Interfaces.Repository.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArquitectChallenge.Services.Repository.Events
{
    public class EventRepository : BaseRepository<EventData>, IEventRepository
    {
        public EventRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IList<GroupEventByTag> GetAllGroupedByTag()
        {
            var groupped = _dataContext.Events.AsNoTracking()
                                .Where(x => !string.IsNullOrWhiteSpace(x.Tag))
                                .GroupBy(x => x.Tag)
                                .Select(x => new GroupEventByTag
                                {
                                    Tag = x.Key,
                                    Count = x.Count()
                                }).ToList();
            if (groupped.Count > 0)
            {
                groupped.AddRange(groupped.Where(x => !string.IsNullOrWhiteSpace(x.FirstTag()))
                            .GroupBy(x => x.FirstTag()).Select(x => new GroupEventByTag
                            {
                                Tag = x.Key,
                                Count = x.Sum(c => c.Count)
                            }).ToList());

                groupped.AddRange(groupped.Where(x => !string.IsNullOrWhiteSpace(x.SecondTag()))
                            .GroupBy(x => x.SecondTag()).Select(x => new GroupEventByTag
                            {
                                Tag = x.Key,
                                Count = x.Sum(c => c.Count)
                            }).ToList());
            }

            return groupped.OrderBy(x => x.Tag).ToList();
        }

        public override T GetById<T>(int id)
        {
            return _dataContext.Events.Where(x => x.Id == id).FirstOrDefault().ConvertTo<T>();
        }

        public IList<GroupEventByHour> GetEventsGroupedByHour(DateTime date)
        {
            var events = _dataContext.Events
                            .Where(x => x.Timestamp.UnixTimeStampToDateTime().Date == date.Date)
                            .OrderBy(x => x.Tag)
                            .ThenBy(x => x.Timestamp)
                            .GroupBy(x => x.Tag)
                            .ToList();

            var groupped = new List<GroupEventByHour>();
            foreach (var tag in events)
            {
                var eventsByHour = new int[24];
                foreach (var eventData in tag.GroupBy(x => x.Timestamp.UnixTimeStampToDateTime().Hour))
                {
                    if (eventsByHour.Length >= eventData.Key)
                    {
                        eventsByHour[eventData.Key] = eventData.Count();
                    }
                }

                groupped.Add(new GroupEventByHour { Tag = tag.Key, EventsByHour = eventsByHour });
            }

            return groupped;
        }

        public override IList<T> GetList<T>()
        {
            return _dataContext.Events.AsNoTracking().ToList()
                            .OrderByDescending(x => x.Timestamp)
                            .ThenBy(x => x.Tag)
                            .Select(UtilExtensions.ConvertTo<T>)
                            .ToList();
        }

        public IList<EventData> GetNewestEvents(int lastEventId)
        {
            return _dataContext.Events
                            .Where(x => x.Id > lastEventId)
                            .OrderByDescending(x => x.Timestamp)
                            .ThenBy(x => x.Tag)
                            .ToList();
        }

        public IList<EventData> GetNumericEvents()
        {
            return _dataContext.Events
                        .Where(x => x.IsNumeric)
                        .ToList();
        }

        protected override void UpdateItem(EventData currentItem, EventData updatedItem)
        {
            currentItem.Tag = updatedItem.Tag;
            currentItem.Timestamp = updatedItem.Timestamp;
            currentItem.Valor = updatedItem.Valor;
        }
    }
}