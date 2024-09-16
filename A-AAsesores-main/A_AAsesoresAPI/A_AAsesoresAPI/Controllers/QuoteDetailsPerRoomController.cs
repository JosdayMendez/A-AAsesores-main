using A_AAsesoresAPI.Entities;
using System;
using System.Web.Http;


namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class QuoteDetailsPerRoomController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterQuoteDetailsPerRoom")]
        public int RegisterQuoteDetailsPerRoom(QuoteDetailsPerRoomEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertQuoteDetailsPerRoomSP(entidad.QuoteRoomId, entidad.Quantity);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /*

        [HttpPut]
        [Route("UpdateQuoteDetailsPerRoom")]
        public int UpdateQuoteDetailsPerRoom(QuoteDetailsPerRoomEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateQuoteDetailsPerRoomSP(entidad.Id, entidad.QuoteDetailsId, entidad.QuoteRoomId);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeleteQuoteDetailsPerRoom")]
        public int DeleteQuoteDetailsPerRoom(int id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteQuoteDetailsPerRoomSP(id);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("ListQuoteDetailsPerRoom")]
        public List<System.Web.Mvc.SelectListItem> ListQuoteDetailsPerRoom()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetQuoteDetailsPerRoomSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = $"QuoteDetailsId: {item.QuoteDetailsId}, QuoteRoomId: {item.QuoteRoomId}" });
                    }
                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }*/


    }
}