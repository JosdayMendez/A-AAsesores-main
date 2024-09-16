using A_AAsesoresWeb.Models;
using System.Linq;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Controllers
{
    public class AuditLogController : Controller
    {
        readonly AuditLogModel auditLogModel = new AuditLogModel();

        public ActionResult AuditVisualization()
        {
            var auditList = auditLogModel.GetAudit();

            if (!auditList.Any())
            {
                ViewBag.NoDataMessage = "No hay Bitacoras disponibles.";
                return View();
            }

            return View(auditList);
        }
    }
}