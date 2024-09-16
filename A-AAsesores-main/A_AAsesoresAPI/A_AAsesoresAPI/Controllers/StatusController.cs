using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class StatusController : ApiController
    {
        /*Insertar un nuevo Estado*/
        [HttpPost]
        [Route("RegisterStatus")]
        public int RegisterStatus(StatusEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertStatusSP(entidad.Name, entidad.Description, entidad.RelatedTable);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /*Actualizar Estados*/
        [HttpPut]
        [Route("UpdateStatus")]
        public int UpdateStatus(StatusEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateStatusSP(entidad.Id, entidad.Name, entidad.Description, entidad.RelatedTable, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /*Eliminar  Estado*/
        [HttpDelete]
        [Route("DeleteStatus")]
        public int DeleteDocumentType(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteStatusSP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /*Obtiene los estados en formato de Dropdown */
        [HttpGet]
        [Route("ListAllStatus")]
        public List<System.Web.Mvc.SelectListItem> ListAllStatus()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllStatusSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                    }
                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        //Lista de roles en un dropdown list de estatus de usuario
        [HttpGet]
        [Route("ListEmployeeStatuses")]
        public List<System.Web.Mvc.SelectListItem> ListEmployeeStatuses()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetEmployeeStatusesSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                    }
                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        //Lista de roles en un dropdown list de estado de propiedades
        [HttpGet]
        [Route("ListPropertiesStatuses")]
        public List<System.Web.Mvc.SelectListItem> ListPropertiesStatuses()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetPropertiesStatusesSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                    }
                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        //Lista de roles en un dropdown list de status de citas
        [HttpGet]
        [Route("ListAppointmentStatuses")]
        public List<System.Web.Mvc.SelectListItem> ListAppointmentStatuses()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAppointmentStatusesSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Name });
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
        [Route("GetAllStatus")]
        public List<GetStatusSP_Result> GetAllStatus()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetStatusSP().ToList();
            }
        }

        [HttpPut]
        [Route("ChangeStatusStatus")]
        public int ChangeStatusStatus(StatusEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeStatusStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }


        [HttpGet]
        [Route("GetStatusByIdSP")]
        public GetStatusByIdSP_Result GetStatusByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetStatusByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }


    } //Cierre de la clase
} //Cierre del namespace

