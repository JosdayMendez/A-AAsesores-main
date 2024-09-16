using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class AuditLogController : ApiController
    {

        [HttpPost]
        [Route("RegisterAudit")]
        public int RegisterAudit(AuditLogEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertAuditLogSP(entidad.EmployeeId, entidad.CurrentAudit);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPut]
        [Route("UpdateAudit")]
        public int UpdateAudit(AuditLogEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateAuditLogSP(entidad.Id, entidad.EmployeeId, entidad.CurrentAudit);
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }


        [HttpDelete]
        [Route("DeleteAudit/{id}")]
        public int DeleteAudit(int id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteAuditLogSP(id);
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }


        [HttpGet]
        [Route("GetAudit")]
        public List<GetAllAuditLogSP_Result> GetAudit()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetAllAuditLogSP().ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener la lista.", ex);
            }
        }
    }

}

