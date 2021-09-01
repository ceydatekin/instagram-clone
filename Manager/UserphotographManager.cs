using Instagram.InstagramContext;
using Instagram.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Manager
{
    public class UserphotographManager : Instagram.Repository.IRepository<Userphotograph>
    {
        instagramContext _context = ContextManager.GetContext();

        // UserphotographManager kullaniciFotoManager = new UserphotographManager();
        UserfollowersManager kullaniciTakipKontrol = new UserfollowersManager();
        PhotographManager photographManager = new PhotographManager();
        public Userphotograph GetUserFotoid(int resimid) => ContextManager.GetContext().Userphotographs.SingleOrDefault(entity => entity.Photographid == resimid);
        public AkisViewModel AkislariGetir(int sessionid)
        {


            var liste = kullaniciTakipKontrol.takipettiklerimigetir(sessionid);
              


            var akis = from followers in liste
                       join users in ContextManager.GetContext().Users on followers equals users.Id
                       join kullaniciFotograf in ContextManager.GetContext().Userphotographs on followers equals kullaniciFotograf.Userid
                       join fotograflar in ContextManager.GetContext().Photographs on kullaniciFotograf.Photographid equals fotograflar.Id
                       select new
                       {
                           userfoto = photographManager.Getfoto(users.Userphotoid).Photograph1,
                           Name = users.Name + " " + users.Surname,
                           Username = users.Username,
                           Base64 = fotograflar.Photograph1,
                           Text = fotograflar.Photographtext,
                           PhotoId = fotograflar.Id,
                           KullaniciFotografId = kullaniciFotograf.Id,
                           LikeSayisi = ContextManager.GetContext().Likes.Where(s => s.Userphotographid == kullaniciFotograf.Id).ToList().Count,
                           isLiked = ContextManager.GetContext().Likes.SingleOrDefault(s => s.Userphotographid == kullaniciFotograf.Id && s.Userid == sessionid) == null ? false: true
                           //vatandaslarintakipedilmeyenler = kullaniciTakipKontrol.takipetmediklerimigetir(sessionid)
                       };
             
            var listAkisFotogramlar = JsonConvert.DeserializeObject<List<akisfotograflarVievModel>>(JsonConvert.SerializeObject(akis)); 

            var response = new AkisViewModel();
            response.akisfotograflarVievModel = listAkisFotogramlar;
            response.vatandaslarintakipedilmeyenler = kullaniciTakipKontrol.takipetmediklerimigetir(sessionid);

            return response;
        }


        public AkisViewModel ProfilAkisi(int sessionid)
        {


            var liste = kullaniciTakipKontrol.takipettiklerimigetir(sessionid);



            var akis = from followers in liste
                       join users in ContextManager.GetContext().Users on followers equals users.Id
                       join kullaniciFotograf in ContextManager.GetContext().Userphotographs on sessionid equals kullaniciFotograf.Userid
                       join fotograflar in ContextManager.GetContext().Photographs on kullaniciFotograf.Photographid equals fotograflar.Id
                       select new
                       {
                           userfoto = photographManager.Getfoto(users.Userphotoid).Photograph1,
                           Name = users.Name + " " + users.Surname,
                           Username = users.Username,
                           Base64 = fotograflar.Photograph1,
                           Text = fotograflar.Photographtext,
                           PhotoId = fotograflar.Id,
                           KullaniciFotografId = kullaniciFotograf.Id,
                           LikeSayisi = ContextManager.GetContext().Likes.Where(s => s.Userphotographid == kullaniciFotograf.Id).ToList().Count,
                           isLiked = ContextManager.GetContext().Likes.SingleOrDefault(s => s.Userphotographid == kullaniciFotograf.Id && s.Userid == sessionid) == null ? false : true
                           //vatandaslarintakipedilmeyenler = kullaniciTakipKontrol.takipetmediklerimigetir(sessionid)
                       };

            var listAkisFotogramlar = JsonConvert.DeserializeObject<List<akisfotograflarVievModel>>(JsonConvert.SerializeObject(akis));

            var response = new AkisViewModel();
            response.akisfotograflarVievModel = listAkisFotogramlar;
            response.vatandaslarintakipedilmeyenler = kullaniciTakipKontrol.takipetmediklerimigetir(sessionid);

            return response;
        }


        public string FotoEkle(IFormFile file, string photographtext, string user_id)
        {
            using (var ms = new MemoryStream())
            {
                try
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string resimBase64 = Convert.ToBase64String(fileBytes);
                    var resim_id = FotografTablosunaEkle(resimBase64, photographtext);
                    KullaniciFotografTablosunaEkle(resim_id, user_id);
                    return resim_id;
                }
                catch (Exception err)
                {
                    return null;
                }


            }
        }
        public string ProfilFotoEkle(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                try
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string resimBase64 = Convert.ToBase64String(fileBytes);
                    var resim_id = FotografTablosunaEkle(resimBase64, "");
                    return resim_id;
                }
                catch (Exception err)
                {
                    return null;
                }


            }
        }

        private string FotografTablosunaEkle(string resim64, string resimYazisi)
        {
            var entity = new InstagramContext.Photograph
            {
                Photograph1 = resim64,
                Photographtext = resimYazisi


            };
            int userfotoid = photographManager.Insert(entity).Id;
            return entity.Id + "";
        }
        public Userphotograph Insert(Userphotograph entity)
        {

            _context.Set<Userphotograph>().Add(entity);
            Save();
            return entity;

        }
        private void KullaniciFotografTablosunaEkle(string id, string user_id)
        {

   
           Insert(new Userphotograph
            {
                Userid = int.Parse(user_id),
                Photographid = int.Parse(id)
            });

        }

        
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
    }
}
