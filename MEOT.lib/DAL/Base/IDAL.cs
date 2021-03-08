using System;
using System.Collections.Generic;

using MEOT.lib.Objects.Base;

namespace MEOT.lib.DAL.Base
{
    public interface IDAL
    {
        void Insert<T>(T item) where T: BaseObject;

        public List<T> SelectAll<T>() where T : BaseObject;

        void Delete<T>(T item) where T : BaseObject;

        void DeleteWhere<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : BaseObject;

        void DeleteById<T>(int id) where T : BaseObject;

        void Update<T>(T item) where T : BaseObject;

        T SelectOne<T>(int id) where T : BaseObject;

        T SelectFirstOrDefault<T>() where T : BaseObject;
    }
}