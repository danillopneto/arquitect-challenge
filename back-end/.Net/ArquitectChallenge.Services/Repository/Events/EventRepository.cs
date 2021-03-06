﻿using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Domain.Utilities;
using ArquitectChallenge.Interfaces.Repository.Events;
using ArquitectChallenge.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArquitectChallenge.Services.Repository.Events
{
    public class EventRepository : BaseRepository<EventData>, IEventRepository
    {
        public EventRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IList<GroupEventByTag> GetAllGroupedByTag()
        {
            return _dataContext
                        .SqlQuery<GroupEventByTag>(GetSqlGrouppedByTag().ToString())
                        .ToList();
        }

        public override T GetById<T>(int id)
        {
            return _dataContext.Events.Where(x => x.Id == id).FirstOrDefault().ConvertTo<T>();
        }

        public IList<GroupEventByHour> GetEventsGroupedByHour(DateTime date)
        {
            return _dataContext
                        .SqlQuery<GroupEventByHour>(GetSqlGrouppedByHour(date).ToString())
                        .ToList();
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

        public IList<IGrouping<string, EventData>> GetNumericEventsGroupped(DateTime date)
        {
            return _dataContext.Events
                        .Where(x => x.IsNumeric
                                && x.Timestamp.UnixTimeStampToDateTime().Date == date.Date)
                        .GroupBy(x => x.Tag)
                        .ToList();
        }

        protected override void UpdateItem(EventData currentItem, EventData updatedItem)
        {
            currentItem.Tag = updatedItem.Tag;
            currentItem.Timestamp = updatedItem.Timestamp;
            currentItem.Valor = updatedItem.Valor;
        }

        private StringBuilder GetSqlGrouppedByHour(DateTime date)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("	COUNT, ");
            sql.AppendLine("	FINALTAG AS TAG, ");
            sql.AppendLine("    DIA AS `TIMESTAMP`, ");
            sql.AppendLine("    ISREGION ");
            sql.AppendLine("FROM (SELECT ");
            sql.AppendLine("COUNT(*) AS COUNT, ");
            sql.AppendLine("SUBSTRING_INDEX(TAG, '.', 2) AS FINALTAG, ");
            sql.AppendLine("UNIX_TIMESTAMP(FROM_UNIXTIME(`TIMESTAMP` / 1000, '%Y-%m-%d %H:00')) AS DIA, ");
            sql.AppendLine("TRUE AS ISREGION ");
            sql.AppendLine("FROM `EventData` ");
            sql.AppendLine("WHERE ");
            sql.AppendFormat("	FROM_UNIXTIME(`TIMESTAMP` / 1000, '%Y-%m-%d') = '{0}' ", date.ToString("yyyy-MM-dd"));
            sql.AppendLine("GROUP BY FINALTAG, DIA ");
            sql.AppendLine("UNION ");
            sql.AppendLine("SELECT ");
            sql.AppendLine("COUNT(*) AS COUNT, ");
            sql.AppendLine("TAG AS FINALTAG, ");
            sql.AppendLine("UNIX_TIMESTAMP(FROM_UNIXTIME(`TIMESTAMP` / 1000, '%Y-%m-%d %H:00')) AS DIA, ");
            sql.AppendLine("FALSE AS ISREGION ");
            sql.AppendLine("FROM `EventData` ");
            sql.AppendLine("WHERE ");
            sql.AppendFormat("	FROM_UNIXTIME(`TIMESTAMP` / 1000, '%Y-%m-%d') = '{0}' ", date.ToString("yyyy-MM-dd"));
            sql.AppendLine("GROUP BY TAG, DIA) BYHOUR ");
            sql.AppendLine("ORDER BY `TIMESTAMP`, FINALTAG; ");
            return sql;
        }

        private StringBuilder GetSqlGrouppedByTag()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT COUNT, FINALTAG AS TAG FROM(SELECT ");
            sql.AppendLine("COUNT(*) AS COUNT, ");
            sql.AppendLine("SUBSTRING_INDEX(TAG, '.', 1) AS FINALTAG ");
            sql.AppendLine("FROM `EventData` ");
            sql.AppendLine("GROUP BY FINALTAG ");
            sql.AppendLine("UNION ");
            sql.AppendLine("SELECT ");
            sql.AppendLine("COUNT(*) AS COUNT, ");
            sql.AppendLine("SUBSTRING_INDEX(TAG, '.', 2) AS FINALTAG ");
            sql.AppendLine("FROM `EventData` ");
            sql.AppendLine("GROUP BY FINALTAG) PARENTTAGS ");
            sql.AppendLine("UNION ");
            sql.AppendLine("SELECT ");
            sql.AppendLine("COUNT(*) AS COUNT, ");
            sql.AppendLine("TAG ");
            sql.AppendLine("FROM `EventData` TAG ");
            sql.AppendLine("GROUP BY TAG");
            return sql;
        }
    }
}