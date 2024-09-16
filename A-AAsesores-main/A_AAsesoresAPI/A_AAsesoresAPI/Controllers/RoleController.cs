using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class RoleController : ApiController
    {
        /*Insertar un nuevo Role*/
        [HttpPost]
        [Route("RegisterRole")]
        public int RegisterRole(RoleEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertRoleSP(entidad.RoleName, entidad.Description);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /*Actualizar Rol*/
        [HttpPut]
        [Route("UpdateRoleSP")]
        public int UpdateRoleSP(RoleEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateRoleSP(entidad.Id, entidad.RoleName, entidad.Description, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /*Eliminar Tipo de Documento*/
        [HttpDelete]
        [Route("DeleteRole")]
        public int DeleteRole(int id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteRoleSP(id);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /*Obtiene los roles en formato de Dropdown */
        [HttpGet]
        [Route("ListRole")]
        public List<System.Web.Mvc.SelectListItem> ListRole()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllRoleSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    respuesta.Add(new System.Web.Mvc.SelectListItem { Value = "", Text = "Seleccione un rol" });
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.RoleName });
                    }

                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("GetAllRole")]
        public List<GetRoleSP_Result> GetAllRole()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetRoleSP().ToList();
            }
        }

        [HttpPut]
        [Route("ChangeRoleStatus")]
        public int ChangeRoleStatus(RoleEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeRoleStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("GetRoleByIdSP")]
        public GetRoleByIdSP_Result GetRoleByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetRoleByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

      
    }
}
