using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class PropertyImgController : ApiController
    {
        [HttpPost]
        [Route("RegisterPropertyImg")]
        public int RegisterPropertyImg(PropertyImgEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertPropertyImgSP(entidad.ImageUrl, entidad.PropertyId);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPropertiesImgs")]
        public List<GetAllPropertyImgsSP_Result> GetPropertiesImgs()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetAllPropertyImgsSP().ToList();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPropertyImgs")]
        public List<GetPropertyImgsSP_Result> GetPropertyImgs(int q)
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetPropertyImgsSP(q).ToList();
            }
        }

        [HttpPut]
        [Route("UpdatePropertyImg")]
        public int UpdatePropertyImg(PropertyImgEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdatePropertyImgSP(entidad.Id, entidad.ImageUrl, entidad.PropertyId);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeletePropertyImg")]
        public int DeletePropertyImg(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeletePropertyImgSP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
