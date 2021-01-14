using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace ArquitectChallenge.Services.Extensions
{
    public static class SqlQueryExtensions
    {
        public static IList<T> SqlQuery<T>(this DbContext db, Func<T> targetType, string sql, params object[] parameters) where T : class
        {
            return SqlQuery<T>(db, sql, parameters);
        }

        public static IList<T> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
            {
                return db2.Query<T>().FromSql(sql, parameters).ToList();
            }
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private DbConnection con;

            public ContextForQueryType(DbConnection con)
            {
                this.con = con;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //switch on the connection type name to enable support multiple providers
                //var name = con.GetType().Name;

                optionsBuilder.UseMySql(con);

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                var t = modelBuilder.Query<T>();

                //to support anonymous types, configure entity properties for read-only properties
                foreach (var prop in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (!prop.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappedAttribute)))
                    {
                        t.Property(prop.Name);
                    }
                }
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}