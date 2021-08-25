using Instagram.Helpers;
using Instagram.InstagramContext;
using Instagram.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Controllers
{
    
    [ApiController]
    public class userfollowersApiController : ControllerBase
    {


        UserManager usermanager = new UserManager();
        instagramContext _context = ContextManager.GetContext();
        private readonly ILogger<userfollowersApiController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        SessionHelper sessionHelper;

        public userfollowersApiController(ILogger<userfollowersApiController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);

        }


        UserfollowersManager kullanicitakipciManager = new UserfollowersManager(); 
        [Route("Takip")]
        [HttpPost]
        public string Takiple(int takipedilenid)
        {
            var sessionid = int.Parse(sessionHelper.Get("kullanici_id"));
            var entity = kullanicitakipciManager.GetById(s => s.Followedid == takipedilenid && s.Userid == sessionid);
            if(entity == null)
            {
                kullanicitakipciManager.Insert(new Userfollower
                {
                    Followedid = takipedilenid,
                    Userid = sessionid

                });
                return JsonConvert.SerializeObject(new { success = true, message = "Takip edilmeye başlanıldı!" });
            } 
            else
            {
                kullanicitakipciManager.Delete(entity);
                return JsonConvert.SerializeObject(new { success = true, message = "Takipten çıkıldı!" });

            }

        }


        [Route("TakipciSayisiBul")]
        [HttpPost]
        public string TakipciSayisi(int takipedilenid)
        {
            var sessionid = int.Parse(sessionHelper.Get("kullanici_id"));

            return JsonConvert.SerializeObject(new { success = true, takipcisayisi = kullanicitakipciManager.TakipciBilgisiGetir(sessionid), takipedilensayisi= kullanicitakipciManager.TakipciEdilenBilgisiGetir(sessionid) });
        }



    }
}
