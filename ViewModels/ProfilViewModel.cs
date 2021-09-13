using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.ViewModels
{
    public class ProfilViewModel
    {
        public List<profilfotograflarVievModel> profilfotograflarVievModel { get; set; }
        public int FollowersCount { get; set; }
        public int takipettiklerininCount { get; set; }
        public int fotosayisi { get; set; }
        public string username { get; set; }
        public string userfoto { get; set; }
    }

    public class profilfotograflarVievModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Base64 { get; set; }
        public string Text { get; set; }
        public string PhotoId { get; set; }
       

    }
}
