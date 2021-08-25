using Instagram.InstagramContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.ViewModels
{
    public class UserProfilePhotoViewModel
    {
        public User User { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
