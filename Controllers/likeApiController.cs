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
    public class likeApiController : ControllerBase
    {

        UserManager usermanager = new UserManager();
        instagramContext _context = ContextManager.GetContext();
        private readonly ILogger<likeApiController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        SessionHelper sessionHelper;

        public likeApiController(ILogger<likeApiController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);

        }

        LikeManager begenmeKontrol = new LikeManager();
        UserphotographManager userfotokontrol = new UserphotographManager();

        [Route("like")]
        [HttpPost]
        public string LikeKontrol(int resimid)
        {
            var sessionid = int.Parse(sessionHelper.Get("kullanici_id"));
             var userphotograph = userfotokontrol.GetUserFotoid(resimid);

            var check = begenmeKontrol.GetById(entity => entity.Userphotographid == userphotograph.Id && entity.Userid == sessionid);

            if (check == null)
            {
                begenmeKontrol.Insert(new Like { 
                 Userid = sessionid,
                 Userphotographid = userphotograph.Id
               

            });
                return JsonConvert.SerializeObject(new { success = true, message = "begenildi"});
            }
            else
            {
                begenmeKontrol.Delete(check);
                return JsonConvert.SerializeObject(new { success = true, message = "begenialindi" });
            }

          

        }
        [Route("Begenisayisi")]
        [HttpPost]
        public string Begenisayi(int resimid)
        {
            var userphotograph = userfotokontrol.GetUserFotoid(resimid);

            return JsonConvert.SerializeObject(new { success = true, takipcisayisi = begenmeKontrol.BegeniSayisiniGetir(userphotograph.Id )});

        }

    }
}
