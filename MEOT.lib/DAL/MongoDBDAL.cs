using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using MEOT.lib.DAL.Base;
using MEOT.lib.Objects.Base;

using MongoDB.Driver;

namespace MEOT.lib.DAL
{
    public class MongoDBDAL : IDAL
    {
        private string _connectionString = null;

        private const string DBNAME = "MEOT";

        private IMongoDatabase _db;

        public MongoDBDAL(string connectionString)
        {
            _connectionString = connectionString;

            var client = new MongoClient(_connectionString);

            _db = client.GetDatabase(DBNAME);
        }

        public void Insert<T>(T item) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            collection.InsertOne(item);
        }

        public List<T> SelectAll<T>() where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            return collection.Find(a => a != null).ToList();
        }

        public List<T> SelectAll<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            return collection.Find(expression).ToList();
        }

        public void Delete<T>(T item) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            collection.DeleteOne(a => a.Id == item.Id);
        }

        public void DeleteWhere<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            collection.DeleteMany(expression);
        }

        public void DeleteById<T>(int id) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            collection.DeleteOne(a => a.Id == id);
        }

        public void Update<T>(T item) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            collection.ReplaceOne(a => a.Id == item.Id, item);
        }

        public T SelectOne<T>(int id) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            return collection.Find(a => a.Id == id).FirstOrDefault();
        }

        public T SelectOne<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            return collection.Find(expression).FirstOrDefault();
        }

        public T SelectFirstOrDefault<T>() where T : BaseObject
        {
            var collection = _db.GetCollection<T>(typeof(T).Name);

            return collection.Find(a => a != null).FirstOrDefault();
        }
    }
}
