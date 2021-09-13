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
using Newtonsoft.Json;

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
        [Route("Home/Index")]
        [Route("")]
        public IActionResult Index()
        {
            //var aa = sessionHelper.Get("kullanici_id");

            var a = ViewBag.ct;
            // hey theree
            //
            var currentUser = sessionHelper.Get("kullanici_adi");
            if (String.IsNullOrEmpty(sessionHelper.Get("kullanici_adi")))
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

            return View(userphotographManager.ProfilAkisi(int.Parse(sessionHelper.Get("kullanici_id")),int.Parse(sessionHelper.Get("kullanici_foto"))));
        }

        public IActionResult Login()
        {
            // Tamam gibii :)
            //çok teşekkür ederimmmm
            // burada yanlış viewe döndüğü için infinite loop olmuş. LOgin indexe , inde logine atıyr. 
            // anladım ama bir sorum var biz session null değilse index demiştik orda nullsa login dediğimiz içinmi oldu
            // evet. çünkü çıkıtaş boşluk atıyoruz, null yapmıyoruzz
            //anladımmm şimdi ! Harikaaaa 
            // şimdi ki adımımız nedir ?
            //size soracaktım 
            if (String.IsNullOrEmpty(sessionHelper.Get("kullanici_adi")))
            {
                // Eğer kullanıcı giriş yapmadıysa, Login View yönlenecek
                return View();

            }
            // Eğer kullancıı giriş yaptıysa, Anasayfaaa
            return RedirectToAction("Index");
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
                sessionHelper.Set("kullanici_foto", user.Userphotoid);

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



        [Route("cikisyap")]
        [HttpPost]
        public IActionResult logout()
        {
            sessionHelper.Set("kullanici_adi", "");
            return View("Login");

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
