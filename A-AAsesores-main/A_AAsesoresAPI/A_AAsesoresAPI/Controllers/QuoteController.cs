using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class QuoteController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterQuote")]
        public int RegisterQuote(QuoteEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {

                    return context.InsertQuoteSP(entidad.ClientId, entidad.Details);
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        [HttpGet]
        [Route("ConsultQuote")]
        public List<GetQuoteDetails_Result> ConsultQuote()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetQuoteDetails().ToList();

                }
            }
            catch (Exception)
            {
                return new List<GetQuoteDetails_Result>();
            }
        }

        [HttpPut]
        [Route("ChangeQuoteStatus")]
        public int ChangeQuoteStatus(int? id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeQuoteStatusSP(id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPost]
        [Route("AddDocUrl")]
        public int AddDocUrl(QuoteEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {

                    return context.AddDocUrlToQuote(entidad.Id, entidad.DocUrl);
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        [HttpPut]
        [Route("DeleteDocUrl")]
        public int DeleteDocUrl(int id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {

                    context.DeleteDocUrlFromQuote(id);
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
