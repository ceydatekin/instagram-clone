using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.InstagramContext;
using Instagram.Repository;

namespace Instagram.Manager
{
    public class UserManager : IRepository<User>
    { 
        public User GetUser(string username, string password) => ContextManager.GetContext().Users.SingleOrDefault(entity => entity.Username == username && entity.Password == password);
    }
}
