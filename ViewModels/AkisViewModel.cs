using Instagram.InstagramContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.ViewModels
{
    public class AkisViewModel
    {
        public List<akisfotograflarVievModel> akisfotograflarVievModel { get; set; }
        public List<UserProfilePhotoViewModel> vatandaslarintakipedilmeyenler { get; set; }
    }
    public class akisfotograflarVievModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Base64 { get; set; }
        public string Text { get; set; }
        public string PhotoId { get; set; }
        public string KullaniciFotografId { get; set; }
        public string LikeSayisi { get; set; }
        public string userfoto { get; set; }
        public bool isLiked { get; set; }
    }

}
