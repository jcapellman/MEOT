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

        public void CreateOrUpdate(User user, bool create)
        {
            if (create)
            {
                _db.Insert(user);
            }
            else
            {
                _db.Update(user);
            }
        }

        public User GetUserById(int id) => _db.SelectOne<User>(id);

        public void DeleteUserById(int id) => _db.DeleteById<User>(id);
    }
}