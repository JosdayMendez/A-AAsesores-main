using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class NewsController : ApiController
    {
        
        [HttpPost]
        [Route("RegisterNews")]
        public int RegisterNews(NewsEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertNewsSP(entidad.EmployeeId, entidad.Title, entidad.Description, entidad.Url,entidad.ImageUrl);
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        [HttpGet]
        [Route("GetNewsByIdSP")]
        public GetNewsByIdSP_Result GetNewsByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetNewsByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPut]
        [Route("UpdateNews")]
        public int UpdateNews(NewsEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateNewsSP(entidad.Id, entidad.EmployeeId, entidad.Title, entidad.Description, entidad.Url,entidad.ImageUrl);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPut]
        [Route("UpdateNewsImg")]
        public int UpdateNewsImg(NewsEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateUrlImgSP(entidad.Id,entidad.ImageUrl);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }


        [HttpDelete]
        [Route("DeleteNews/{id}")]
        public int DeleteNews(int id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteNewsSP(id);
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ListNews")]
        public List<GetAllNewsSP_Result> ListNews()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetAllNewsSP().ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener la lista.", ex);
            }
        }
    }
}
