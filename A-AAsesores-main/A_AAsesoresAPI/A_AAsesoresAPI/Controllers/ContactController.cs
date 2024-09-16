using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class ContactController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterContact")]
        public int RegisterContact(ContactEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertContactSP(entidad.UserId, entidad.Message);
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        [HttpGet]
        [Route("ListContacts")]
        public List<GetContactsSP_Result> ListContacts()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetContactsSP().ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener la lista.", ex);
            }
        }

        [HttpPut]
        [Route("ChangeContactStatus")]
        public int ChangeContactStatus(int? id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeContactStatusSP(id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}