using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class PropertyTypeController : ApiController
    {
        [HttpPost]
        [Route("RegisterPropertyType")]
        public int RegisterPropertyType(PropertyTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertPropertyTypeSP(entidad.Type, entidad.Description);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPropertyTypes")]
        public List<GetAllPropertyTypesSP_Result> GetPropertyTypes()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetAllPropertyTypesSP().ToList();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllPropertyTypes")]
        public List<GetPropertyTypesSP_Result> GetAllPropertyTypes()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetPropertyTypesSP().ToList();
            }
        }

        [HttpPut]
        [Route("UpdatePropertyType")]
        public int UpdatePropertyType(PropertyTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdatePropertyTypeSP(entidad.Id, entidad.Type, entidad.Description, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeletePropertyType")]
        public int DeletePropertyType(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeletePropertyTypeSP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("ListPropertyTypes")]
        public List<System.Web.Mvc.SelectListItem> ListPropertyTypes()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllPropertyTypesSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    respuesta.Add(new System.Web.Mvc.SelectListItem { Value = "", Text = "Por favor seleccione" });
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Type });
                    }
                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpPut]
        [Route("ChangePropertyTypeStatus")]
        public int ChangePropertyTypeStatus(PropertyEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangePropertyTypeStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("GetPropertyTypesByIdSP")]
        public GetPropertyTypeByIdSP_Result GetPropertyTypesByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetPropertyTypeByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}

