using System;
using System.Collections.Generic;

#nullable disable

namespace Instagram.InstagramContext
{
    public partial class Like
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public int? Userphotographid { get; set; }
    }
}
