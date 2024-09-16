using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class AppointmentController : ApiController
    {
        /*Insertar*/
        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterAppointment")]
        public int RegisterAppointment(AppointmentEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertAppointmentSP(entidad.AppointmentDate, entidad.UserId,entidad.PropertyId);
                }
            }
            catch (Exception)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la ejecución del stored procedure
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAppointments")]
        public List<GetAllAppoinmentsSP_Result> GetAppointments()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetAllAppoinmentsSP().ToList();
                }
            }
            catch (Exception)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la ejecución del stored procedure
                return new List<GetAllAppoinmentsSP_Result>();
            }
        }

        [HttpPut]
        [Route("ChangeAppointmentStatus")]
        public int ChangeAppointmentStatus(AppointmentEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeAppointmentStatus(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        /*Consultar un appointment en especifico*/
        [HttpGet]
        [Route("ConsultAppointment")]
        public GetAppointmentSP_Result ConsultAppointment(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetAppointmentSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        //Actualizar un appointment
        [HttpPut]
        [Route("UpdateAppointment")]
        public int UpdateAppointment(AppointmentEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateAppointmentSP(entidad.Id,entidad.Status,entidad.EmployeeId,entidad.AppointmentDate);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
