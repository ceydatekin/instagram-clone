using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Manager
{
    public static class ManagerCreator
    {
        private static LikeManager _likeManager;
        public static LikeManager CreateLikeManager()
        {
            if(_likeManager is null)
            {
                _likeManager = new LikeManager();
            }
            return _likeManager;
        }
    }
}
