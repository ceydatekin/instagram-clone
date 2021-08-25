using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.InstagramContext;
namespace Instagram.Manager
{
    public class ContextManager
    {
        private static instagramContext context;
        private ContextManager()
        {

        }
        public static instagramContext GetContext() {
            if (context == null)
                context = new instagramContext();

            return context;
        }
    }
}
