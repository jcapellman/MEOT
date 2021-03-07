using System.Collections.Generic;

using MEOT.lib.Objects.Base;

namespace MEOT.lib.DAL.Base
{
    public interface IDAL
    {
        void Insert<T>(T item) where T: BaseObject;

        public List<T> SelectAll<T>() where T : BaseObject;
    }
}