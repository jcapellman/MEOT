using System;
using System.Collections.Generic;
using System.Linq;

using MEOT.lib.DAL.Base;
using MEOT.lib.Objects.Base;

namespace MEOT.lib.DAL
{
    public class LiteDBDAL : IDAL
    {
        private const string DB_NAME = "meot_litedb.db";

        public void Insert<T>(T item) where T: BaseObject
        {
            using var db = new LiteDB.LiteDatabase(DB_NAME);

            var collection = db.GetCollection<T>();

            collection.Insert(item);
        }

        public List<T> SelectAll<T>() where T : BaseObject
        {
            using var db = new LiteDB.LiteDatabase(DB_NAME);

            var collection = db.GetCollection<T>();

            return collection.FindAll().ToList();
        }

        public void Delete<T>(T item) where T : BaseObject
        {
            using var db = new LiteDB.LiteDatabase(DB_NAME);

            var collection = db.GetCollection<T>();

            collection.Delete(item.Id);
        }

        public void DeleteById<T>(int id) where T : BaseObject
        {
            using var db = new LiteDB.LiteDatabase(DB_NAME);

            var collection = db.GetCollection<T>();

            collection.Delete(id);
        }

        public void Update<T>(T item) where T : BaseObject
        {
            using var db = new LiteDB.LiteDatabase(DB_NAME);

            var collection = db.GetCollection<T>();

            item.Modified = DateTimeOffset.Now;
            
            collection.Update(item);
        }

        public T SelectOne<T>(int id) where T : BaseObject
        {
            using var db = new LiteDB.LiteDatabase(DB_NAME);

            var collection = db.GetCollection<T>();

            return collection.FindById(id);
        }

        public T SelectFirstOrDefault<T>() where T : BaseObject
        {
            using var db = new LiteDB.LiteDatabase(DB_NAME);

            var collection = db.GetCollection<T>();

            return collection.FindOne(a => a != null);
        }
    }
}