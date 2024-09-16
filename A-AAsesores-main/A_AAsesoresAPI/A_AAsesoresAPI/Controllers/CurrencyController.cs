using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class CurrencyController : ApiController
    {
        [HttpPost]
        [Route("RegisterCurrency")]
        public int RegisterCurrency(CurrencyEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertCurrencySP(entidad.Name, entidad.CurrencyCode, entidad.Symbol);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("GetAllCurrencies")]
        public List<GetAllCurrenciesSP_Result> GetAllCurrencies()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetAllCurrenciesSP().ToList();
            }
        }

        [HttpGet]
        [Route("GetCurrencyByIdSP")]
        public GetCurrencyByIdSP_Result GetCurrencyByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetCurrencyByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPut]
        [Route("UpdateCurrency")]
        public int UpdateCurrency(CurrencyEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateCurrencySP(entidad.Id, entidad.Name, entidad.CurrencyCode, entidad.Symbol);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeleteCurrency")]
        public int DeleteCurrency(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteCurrencySP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("ListCurrencies")]
        public List<System.Web.Mvc.SelectListItem> ListCurrencies()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllCurrenciesSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Symbol });
                    }
                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
    }
}
