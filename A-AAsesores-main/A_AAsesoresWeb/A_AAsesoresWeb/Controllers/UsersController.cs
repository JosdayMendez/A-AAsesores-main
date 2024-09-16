using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using System.Linq;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Controllers
{
    public class UsersController : Controller
    {
        readonly UserModel userModel = new UserModel();


        //Metodo ára obtnener todos los usuarios si existe en bd y retornarlo a la vista en formato json con ajax
        [HttpGet]
        public ActionResult GetUserByIdentification(string q)
        {
            var datos = userModel.GetUserByIdentification(q);
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        //Obtener todos los clientes
        [HttpGet]
        public ActionResult ConsultClients()
        {
            var datos = userModel.ConsultClients();
            return View(datos);
        }
    }
}