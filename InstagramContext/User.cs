using System;
using System.Collections.Generic;

#nullable disable

namespace Instagram.InstagramContext
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Telno { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int? Userphotoid { get; set; }
    }
}
