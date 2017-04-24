using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;


namespace SweetHelpers.Linq
{
    public static class LinqExtensions
    {
        [Obsolete("Use the MoreLinq batch function instead", true)]
        public static IEnumerable<IQueryable<T>> Chunk<T>(this IQueryable<T> source, int chunkSize)
        {
            var index = 0;
            var count = source.Count();
            while (index < count)
            {
                yield return source.Skip(index).Take(chunkSize);
                index += chunkSize;
            }
        }

        public static IEnumerable<dynamic> CollectionFromSql(this DbContext dbContext, string Sql)
        {
            return dbContext.CollectionFromSql(Sql, new Dictionary<string, object>());
        }
        public static IEnumerable<dynamic> CollectionFromSql(this DbContext dbContext, string Sql, Dictionary<string, object> Parameters)
        {
            using (var cmd = dbContext.Database.Connection.CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                foreach (KeyValuePair<string, object> param in Parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                //var retObject = new List<dynamic>();
                using (var dataReader = cmd.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;

                    }
                }


            }
        }

        private static dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
            return dataRow;
        }


    }
}

namespace Extensions
{
    [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Linq")]
    public static class LinqExtensions
    {
        [Obsolete("Use the MoreLinq batch function instead",true)]
        public static IEnumerable<IQueryable<T>> Chunk<T>(this IQueryable<T> source, int chunkSize)
        {
            var index = 0;
            var count = source.Count();
            while (index < count)
            {
                yield return source.Skip(index).Take(chunkSize);
                index += chunkSize;
            }
        }

        public static IEnumerable<dynamic> CollectionFromSql(this DbContext dbContext, string Sql)
        {
            return dbContext.CollectionFromSql(Sql, new Dictionary<string, object>());
        }
        public static IEnumerable<dynamic> CollectionFromSql(this DbContext dbContext, string Sql, Dictionary<string, object> Parameters)
        {
            using (var cmd = dbContext.Database.Connection.CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                foreach (KeyValuePair<string, object> param in Parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                //var retObject = new List<dynamic>();
                using (var dataReader = cmd.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;

                    }
                }


            }
        }

        private static dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
            return dataRow;
        }


    }
}