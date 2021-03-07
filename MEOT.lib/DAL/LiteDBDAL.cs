using System.Collections.Generic;

using MEOT.lib.DAL.Base;
using MEOT.lib.Objects.Base;

namespace MEOT.lib.DAL
{
    public class LiteDBDAL : IDAL
    {
        private const string DB_NAME = "meot_litedb.db";

        public void InsertAsync(BaseObject item)
        {
            
        }

        public List<BaseObject> SelectAll<T>() where T : BaseObject
        {
            return null;
        }
    }
}