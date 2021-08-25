using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Instagram.Manager;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Instagram.InstagramContext;
using Microsoft.Extensions.Logging;
using Instagram.Helpers;
using Instagram.ViewModels;

namespace Instagram.Controllers
{
 
    //aşağıdaki yazılmadığında da hata almazsın :)
  
    public class photographApiController : Controller
    {


        UserManager usermanager = new UserManager();
        instagramContext _context = ContextManager.GetContext();
        private readonly ILogger<photographApiController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        SessionHelper sessionHelper;

        public photographApiController(ILogger<photographApiController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);
          
        }


        PhotographManager fotoManager = new PhotographManager();
        UserphotographManager kullaniciFotoManager = new UserphotographManager();
        
        [Route ("fotoEkle")]
        [HttpPost]
        public string FotoKayitAsync(IFormFile file, string photographtext)
        {


          return kullaniciFotoManager.FotoEkle(file, photographtext, _httpContextAccessor.HttpContext.Session.GetString("kullanici_id"));
        }
      




    }
}
