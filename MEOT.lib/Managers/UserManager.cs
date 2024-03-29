﻿using System.Collections.Generic;

using MEOT.lib.Common;
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

        public string CreateOrUpdate(User user, bool create)
        {
            var existingUser = _db.SelectOne<User>(a => a.EmailAddress == user.EmailAddress);

            if (existingUser != null)
            {
                if (create || user.Id != existingUser.Id)
                {
                    return "Email Address is already in use";
                }
            }

            user.Password = user.Password.ToSHA256();

            if (create)
            {
                _db.Insert(user);
            }
            else
            {
                _db.Update(user);
            }

            return string.Empty;
        }

        public User GetUserById(int id) => _db.SelectOne<User>(id);

        public void DeleteUserById(int id) => _db.DeleteById<User>(id);

        public string AttemptLogin(User user)
        {
            var hashedPassword = user.Password.ToSHA256();

            if (_db.SelectOne<User>(a => a.EmailAddress == user.EmailAddress && a.Password == hashedPassword) == null)
            {
                return "Invalid username and or password";
            }

            return string.Empty;
        }
    }
}