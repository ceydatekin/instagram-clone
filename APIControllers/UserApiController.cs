using Instagram.InstagramContext;
using Instagram.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Controllers
{

    [ApiController]
    public class UserApiController : ControllerBase
    {
        UserManager userManager = new UserManager();
        UserphotographManager userphotographManager = new UserphotographManager();
        [HttpPost]
        [Route("KullaniciKaydet")]
        public string kullaniciKayit([FromForm] KullaniciKayit model)
        {

            var profilePhotoId = userphotographManager.ProfilFotoEkle(model.profilePhotoFile);

            var kaydidilenKullanici = userManager.Insert(new User
            {
                Name = model.name,
                Surname = model.surname,
                Username = model.userName,
                Telno = model.telno,
                Mail = model.mail,
                Password = model.password,
                Userphotoid = int.Parse(profilePhotoId)
            });

            return JsonConvert.SerializeObject(new { success = true, message = "Tebrikkler" });
        }
        [HttpGet]
        [Route("KullaniciKaydet")]
        public string kullaniciKayitt([FromForm] KullaniciKayit model)
        { 
            return JsonConvert.SerializeObject(new { success = true, message =model.name });
        }

    }


    public class KullaniciKayit
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string userName { get; set; }
        public string telno { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public IFormFile profilePhotoFile { get; set; }

    }

}
