using System.Collections.Generic;

using MEOT.lib.Objects.Base;

namespace MEOT.lib.DAL.Base
{
    public interface IDAL
    {
        void InsertAsync(BaseObject item);

        public List<BaseObject> SelectAll<T>() where T : BaseObject;
    }
}