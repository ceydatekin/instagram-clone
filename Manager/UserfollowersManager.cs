using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.InstagramContext;
using Instagram.Repository;
using Instagram.ViewModels;
using Newtonsoft.Json;

namespace Instagram.Manager
{
    public class UserfollowersManager:IRepository<Userfollower>
    {
        public int TakipciBilgisiGetir(int userid) => ContextManager.GetContext().Userfollowers.Where(c => c.Followedid == userid).ToList().Count;
        public int TakipciEdilenBilgisiGetir(int userid) => ContextManager.GetContext().Userfollowers.Where(s => s.Userid == userid).ToList().Count;
        public List<int?> takipettiklerimigetir(int sessionid) => ContextManager.GetContext().Userfollowers.Where(s => s.Userid == sessionid).Select(x => x.Followedid).ToList();
        public List<UserProfilePhotoViewModel> takipetmediklerimigetir(int sessionid) {

           var users =  ContextManager.GetContext().Users.Where(s => s.Id != sessionid && !(takipettiklerimigetir(sessionid).Contains(s.Id))).ToList();

            var profileUserPhotos = from _users in users
                                    join photo in ContextManager.GetContext().Photographs on _users.Userphotoid equals photo.Id
                                    select new
                                    {
                                        User = _users,
                                        ProfilePhoto = photo.Photograph1
                                    };
           return JsonConvert.DeserializeObject<List<UserProfilePhotoViewModel>>(JsonConvert.SerializeObject(profileUserPhotos));
        }

    }
}
