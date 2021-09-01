using Instagram.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Helpers;
using Instagram.InstagramContext;
using Instagram.Manager;

namespace Instagram.Controllers
{
    public class HomeController : Controller
    {
        UserManager usermanager = new UserManager();
        instagramContext _context = ContextManager.GetContext();
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        SessionHelper sessionHelper;
        UserphotographManager userphotographManager = new UserphotographManager();
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);
            sessionHelper.Set("kullaniciid", "10");

        }
        [HttpGet]
        public IActionResult Index()
        {
            //var aa = sessionHelper.Get("kullanici_id");


            if (sessionHelper.Get("kullanici_adi") == null)
            {
                return RedirectToAction("Login");
            }

            return View(userphotographManager.AkislariGetir(int.Parse(sessionHelper.Get("kullanici_id"))));
        }
        
     
        public IActionResult Profil()
        {


            if (sessionHelper.Get("kullanici_adi") == null)
            {
                return RedirectToAction("Login");
            }

            return View(userphotographManager.ProfilAkisi(int.Parse(sessionHelper.Get("kullanici_id"))));
        }
        
        public IActionResult Login()
        {

            if (sessionHelper.Get("kullanici_adi") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [Route("girisYapPost")]
        [HttpPost]
        public bool girisYapPost([FromForm] KullaniciKontrolModel model)
        {

            var user = usermanager.GetUser(model.kullaniciAdi,model.sifre);
            if (user == null)
            {
                return false;
            }
            else
            {
                sessionHelper.Set("kullanici_adi", model.kullaniciAdi);
                sessionHelper.Set("kullanici_id", user.Id);
                return true;

            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }


    public class KullaniciKontrolModel
    {
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }
    }
    public class OgretmenOgrenciViewModel
    {
        public List<User> Users = new List<User>();
        public List<Userfollower> Userfollowers = new List<Userfollower>();
        public List<Userphotograph> Userphotograph = new List<Userphotograph>();
        public List<Photograph> Photograph = new List<Photograph>();
        public List<Like> Like = new List<Like>();


    }

}
