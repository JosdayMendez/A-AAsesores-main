using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class QuoteDetailsController : ApiController
    {
        /*[HttpPost]
        [Route("RegisterQuoteDetails")]
        public int RegisterQuoteDetails(QuoteDetailsEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    // Configura los parámetros para el stored procedure
                    var parameters = new SqlParameter[]
                    {
                new SqlParameter("@WalkInClosetQuantity", entidad.WalkInClosetQuantity),
                new SqlParameter("@SecondaryRooms", entidad.SecondaryRooms),
                new SqlParameter("@SecondaryBathrooms", entidad.SecondaryBathrooms),
                new SqlParameter("@Details", entidad.Details),
                // El parámetro de salida para el ID generado por el stored procedure
                new SqlParameter("@ID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }
                    };
                    // Ejecuta el stored procedure
                    context.Database.ExecuteSqlCommand("exec InsertQuoteDetailsSP @WalkInClosetQuantity, @SecondaryRooms, @SecondaryBathrooms, @Details, @ID OUTPUT", parameters);
                    // Recupera el valor de salida después de ejecutar el stored procedure
                    int generatedID = Convert.ToInt32(parameters[4].Value);
                    return generatedID;
                }
            }
            catch (Exception)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la ejecución del stored procedure
                return -1;
            }
        }


        [HttpPut]
        [Route("UpdateQuoteDetails")]
        public int UpdateQuoteDetails(QuoteDetails entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateQuoteDetailsSP(entidad.Id, entidad.WalkInClosetQuantity, entidad.SecondaryRooms, entidad.SecondaryBathrooms, entidad.Details);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeleteQuoteDetails")]
        public int DeleteQuoteDetails(System.Int32 id)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteQuoteDetailsSP(id);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }


        [HttpGet]
        [Route("ListQuoteDetails")]
        public List<System.Web.Mvc.SelectListItem> ListQuoteDetails()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetQuoteDetailsSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Details });
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