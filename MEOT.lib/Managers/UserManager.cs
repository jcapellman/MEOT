using System.Collections.Generic;

using MEOT.lib.DAL.Base;
using MEOT.lib.Objects;

namespace MEOT.lib.Managers
{
    public class UserManager
    {
        private readonly IDAL _db;

        public UserManager(IDAL db)
        {
            _db = db;
        }

        public List<User> GetUsers() => _db.SelectAll<User>();
    }
}