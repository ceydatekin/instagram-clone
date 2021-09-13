using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.InstagramContext;
using Instagram.Repository;

namespace Instagram.Manager
{
    public class PhotographManager:IRepository<Photograph>
    {
        public int FotosayisiniGetir(int userid) => ContextManager.GetContext().Userphotographs.Where(s => s.Userid== userid).ToList().Count;
        public Photograph Getfoto(int? userfotoid) => ContextManager.GetContext().Photographs.SingleOrDefault(entity => entity.Id == userfotoid);
    }
}
