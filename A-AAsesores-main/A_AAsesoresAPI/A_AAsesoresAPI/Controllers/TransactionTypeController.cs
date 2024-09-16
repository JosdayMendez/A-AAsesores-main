using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class TransactionTypeController : ApiController
    {
        [HttpPost]
        [Route("RegisterTransactionType")]
        public int RegisterTransactionType(TransactionTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertTransactionTypeSP(entidad.Type, entidad.Description);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetTransactionTypes")]
        public List<GetAllTransactionTypesSP_Result> GetTransactionTypes()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetAllTransactionTypesSP().ToList();
            }
        }

        [HttpGet]
        [Route("GetAllTransactionTypes")]
        public List<GetTransactionTypesSP_Result> GetAllTransactionTypes()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetTransactionTypesSP().ToList();
            }
        }

        [HttpPut]
        [Route("UpdateTransactionType")]
        public int UpdateTransactionType(TransactionTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateTransactionTypeSP(entidad.Id, entidad.Type, entidad.Description, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeleteTransactionType")]
        public int DeleteTransactionType(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteTransactionTypeSP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("ListTransactionTypes")]
        public List<System.Web.Mvc.SelectListItem> ListTransactionTypes()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllTransactionTypesSP()).ToList();

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
        [Route("ChangeTransactionTypesStatus")]
        public int ChangeTransactionTypesStatus(TransactionTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeTransactionTypeStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }


        [HttpGet]
        [Route("GetTransactionTypesByIdSP")]
        public GetTransactionTypeByIdSP_Result GetTransactionTypesByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetTransactionTypeByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
