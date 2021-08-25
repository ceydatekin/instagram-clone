using System;
using System.Collections.Generic;

#nullable disable

namespace Instagram.InstagramContext
{
    public partial class Userfollower
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public int? Followedid { get; set; }
    }
}
