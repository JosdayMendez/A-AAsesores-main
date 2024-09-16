using A_AAsesoresWeb.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Controllers
{
    public class PrequalificationController : AlertsController
    {


        public ActionResult AddPrequalification()
        {
            //int employeeId;

            //if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            //{
            //    employeeId = (int)Session["IdTEmployee"];
            //}
            //else
            //{
            //    return RedirectToAction("LoginEmployee", "Employee");
            //}

            //Session["IdTEmployee"] = employeeId;

            TempData.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase archivo)
        {
            //int employeeId;

            //if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            //{
            //    employeeId = (int)Session["IdTEmployee"];
            //}
            //else
            //{
            //    return RedirectToAction("LoginEmployee", "Employee");
            //}

            //Session["IdTEmployee"] = employeeId;

            try
            {
                if (archivo == null || archivo.ContentLength <= 0)
                {
                    return Json(new { success = false, message = "No se seleccionó ningún archivo." });
                }

                var nombreArchivo = Path.GetFileName(archivo.FileName);
                var rutaDirectorio = Server.MapPath("~/AppContent/PrequalificationTemplate");
                var rutaAlmacenamiento = Path.Combine(rutaDirectorio, nombreArchivo);

                archivo.SaveAs(rutaAlmacenamiento);

                if (System.IO.File.Exists(rutaAlmacenamiento))
                {
                    return Json(new { success = true, message = "Archivo cargado y almacenado con éxito." });
                }
                else
                {
                    return Json(new { success = false, message = "Error al almacenar el archivo." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al cargar el archivo: " + ex.Message });
            }
            finally
            {
                TempData.Clear();
            }
        }

        public ActionResult ViewFile(string fileName)
        {
            var rutaArchivo = Path.Combine(Server.MapPath("~/AppContent/PrequalificationTemplate"), fileName);

            if (System.IO.File.Exists(rutaArchivo))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(rutaArchivo);

                string contentType = MimeMapping.GetMimeMapping(fileName);

                return File(fileBytes, contentType);
            }
            else
            {
                Alert("Archivo no encontrado.", AlertModel.NotificationType.error);
                return Content("Archivo no encontrado");
            }
        }

        public ActionResult PrequalificationAdmin()
        {

            //int employeeId;

            //if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            //{
            //    employeeId = (int)Session["IdTEmployee"];

            //}
            //else
            //{
            //    return RedirectToAction("LoginEmployee", "Employee");
            //}

            //Session["IdTEmployee"] = employeeId;

            var rutaDirectorio = Server.MapPath("~/AppContent/PrequalificationTemplate");
            var archivos = Directory.GetFiles(rutaDirectorio).Select(Path.GetFileName).ToList();

            return View(archivos);
        }


        public ActionResult Prequalification()
        {


            var rutaDirectorio = Server.MapPath("~/AppContent/PrequalificationTemplate");
            var archivos = Directory.GetFiles(rutaDirectorio).Select(Path.GetFileName).ToList();

            return View(archivos);
        }

        public ActionResult DownloadFile(string fileName)
        {
            var rutaArchivo = Path.Combine(Server.MapPath("~/AppContent/PrequalificationTemplate"), fileName);

            if (System.IO.File.Exists(rutaArchivo))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(rutaArchivo);
                return File(fileBytes, MimeMapping.GetMimeMapping(fileName), fileName);
            }
            else
            {
                Alert("Archivo no encontrado.", AlertModel.NotificationType.error);
                return Content("Archivo no encontrado");
            }
        }

        [HttpPost]
        public ActionResult DeleteFile(string fileName)
        {
            //int employeeId;

            //if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            //{
            //    employeeId = (int)Session["IdTEmployee"];

            //}
            //else
            //{
            //    return RedirectToAction("LoginEmployee", "Employee");
            //}

            //Session["IdTEmployee"] = employeeId;

            try
            {
                var rutaArchivo = Path.Combine(Server.MapPath("~/AppContent/PrequalificationTemplate"), fileName);
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                    Alert("Se ha eliminado el archivo con éxito.", AlertModel.NotificationType.success);
                    return Json(new { success = true });
                }
                else
                {
                    Alert("El archivo no existe.", AlertModel.NotificationType.error);
                    return Json(new { success = false });
                }
            }
            catch (Exception)
            {
                Alert("Error al eliminar el archivo.", AlertModel.NotificationType.error);
                return Json(new { success = false });
            }
            finally
            {
                TempData.Clear();
            }
        }

        public ActionResult DownloadAllFiles()
        {

            var rutaDirectorio = Server.MapPath("~/AppContent/PrequalificationTemplate");
            var archivos = Directory.GetFiles(rutaDirectorio);
            var zipFileName = "PrequalificationFiles.zip";

            using (var zip = new Ionic.Zip.ZipFile())
            {
                foreach (var archivo in archivos)
                {
                    zip.AddFile(archivo, "");
                }

                var memoryStream = new MemoryStream();
                zip.Save(memoryStream);

                return File(memoryStream.ToArray(), "application/zip", zipFileName);
            }
        }
    }
}
