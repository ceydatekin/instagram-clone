using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.InstagramContext;
using Instagram.Repository;

namespace Instagram.Manager
{
    public class LikeManager:IRepository<Like>
    {
        public int BegeniSayisiniGetir(int user_fotoid) => ContextManager.GetContext().Likes.Where(s => s.Userphotographid == user_fotoid).ToList().Count;

    }
}
