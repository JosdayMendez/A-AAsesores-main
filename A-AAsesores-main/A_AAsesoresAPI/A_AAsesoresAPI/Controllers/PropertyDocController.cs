using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class PropertyDocController : ApiController
    {
        [HttpPost]
        [Route("RegisterPropertyDoc")]
        public int RegisterPropertyDoc(PropertyDocEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertPropertyDocSP(entidad.Name, entidad.DocUrl, entidad.PropertyId);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("GetPropertiesDocs")]
        public List<GetAllPropertyDocsSP_Result> GetPropertiesDocs()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetAllPropertyDocsSP().ToList();
            }
        }

        [HttpGet]
        [Route("GetPropertyDocs")]
        public List<GetPropertyDocsSP_Result> GetPropertyImgs(int q)
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetPropertyDocsSP(q).ToList();
            }
        }

        [HttpPut]
        [Route("UpdatePropertyDoc")]
        public int UpdatePropertyDoc(PropertyDocEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdatePropertyDocSP(entidad.Id, entidad.Name, entidad.DocUrl, entidad.PropertyId);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeletePropertyDoc")]
        public int DeletePropertyDoc(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeletePropertyDocSP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
