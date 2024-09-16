using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Controllers
{
    public class QuotationsController : AlertsController
    {
        readonly QuoteModel quote = new QuoteModel();
        readonly QuoteDetailsModel quoteDetails = new QuoteDetailsModel();
        readonly QuoteDetailsPerRoomModel quoteDetailsPerRoom = new QuoteDetailsPerRoomModel();
        readonly QuoteRoomModel quoteRoom = new QuoteRoomModel();
        readonly StatusModel status = new StatusModel();
        readonly UserModel user = new UserModel();
        readonly DocumentTypeModel documentType = new DocumentTypeModel();
        readonly QuoteModel quoteModel = new QuoteModel();
        readonly EmployeeModel employee = new EmployeeModel();
        readonly AlertModel alert = new AlertModel();
        readonly Mail mail = new Mail();


        [HttpPost]
        public ActionResult RegisterQuoteDetails(QuotationsEnt entity)
        {
            int result = quoteDetails.RegisterQuoteDetails(entity.QuoteDetailsEnt);

            if (result != 0)
            {

                return RedirectToAction("RegisterQuoteRooms", "Quotations");
            }
            else
            {

                return RedirectToAction("RegisterQuoteDetails", "Quotations");
            }
        }

        [HttpGet]
        public ActionResult RegisterQuoteRooms()
        {
            var rooms = quoteRoom.GetQuoteRooms();
            return View(rooms);
        }

        [HttpPost]
        public ActionResult RegisterQuotations(QuotationsEnt entidad)
        {
            string patronTel = @"^\d{4}-\d{4}$";
            string patronCorreo = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_Quotations.html";
            string html = System.IO.File.ReadAllText(urlHtml);
            List<string> RoomNames = new List<string>();
            List<int> Quantities = new List<int>();
            QuoteEnt quote = new QuoteEnt();
            ViewBag.QuoteRoom = quoteRoom.GetQuoteRooms();
            ViewBag.Employee = employee.ListEmployeeEmails();
            ViewBag.DocumentType = documentType.ListDocumentType();
            ViewBag.User = user.ListUser();
            if (ModelState.IsValid)
            {
                try
                {
                    int ClientID = 0;
                    bool userFound = false;
                    string patronId = @"^\d{2}-\d{4}-\d{4}$";
                    if (Regex.IsMatch(entidad.UserEnt.Identification, patronId))
                    {
                        foreach (var item in ViewBag.User)
                        {
                            if (item.Text == entidad.UserEnt.Identification.Replace("-", ""))
                            {
                                ClientID = Convert.ToInt32(item.Value);
                                userFound = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        TempData["notification"] = "El formato de la identificación no es correcto.";
                        TempData["notificationType"] = "error";
                        return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }


                    if (!userFound)
                    {
                        if (Regex.IsMatch(entidad.UserEnt.Email, patronCorreo))
                        {
                            ClientID = user.RegisterUser(entidad.UserEnt);
                            if (ClientID == -3)
                            {
                                TempData["notification"] = "El correo electrónico ya se encuentra registrado.";
                                TempData["notificationType"] = "error";
                                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                            }
                        } else { 
                        TempData["notification"] = "Por favor ingrese un correo electrónico válido";
                        TempData["notificationType"] = "error";
                        return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                        }
                    }
                    if (!Regex.IsMatch(entidad.UserEnt.PhoneNumber, patronTel))
                    {
                        TempData["notification"] = "El numero telefónico ingresado no es valido";
                        TempData["notificationType"] = "error";
                        return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                    quote.ClientId = ClientID;
                    quote.Details = entidad.QuoteEnt.Details;
                    int quoteId = quoteModel.RegisterQuote(quote);
                    int pos = 0;
                    foreach (var item in entidad.RoomQuantity)
                    {
                        if (entidad.RoomQuantity[pos] != 0)
                        {
                            Quantities.Add(item);
                        }
                        pos++;
                    }

                    pos = 0;
                    if (entidad.Rooms == null)
                    {
                        TempData["notification"] = "No se seleccionaron habitaciones";
                        TempData["notificationType"] = "error";
                        return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                    else if (entidad.Rooms.Count < 2)
                    {
                        TempData["notification"] = "Se deben de seleccionar mínimo 2 habitaciones";
                        TempData["notificationType"] = "error";
                        return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                    foreach (var item in entidad.Rooms)
                    {
                        QuoteDetailsPerRoomEnt quoteDetailsPerRoomEnt = new QuoteDetailsPerRoomEnt();
                        int id = item;
                        quoteDetailsPerRoomEnt.QuoteRoomId = item;
                        quoteDetailsPerRoomEnt.Quantity = Quantities[pos];
                        int resultt = quoteDetailsPerRoom.RegisterQuoteDetailsPerRoom(quoteDetailsPerRoomEnt);
                        pos++;
                        foreach (var item1 in ViewBag.QuoteRoom)
                        {
                            if (item1.Id == item)
                            {
                                string RoomName = item1.RoomName;
                                RoomNames.Add(RoomName + " " + quoteDetailsPerRoomEnt.Quantity);
                            }
                        }
                    }
                    
                    foreach (var item in ViewBag.Employee)
                    {
                        string email = item.Text;
                        mail.SendMail(email, "Cotización de " + entidad.UserEnt.Name + " " + entidad.UserEnt.FirstLastName, html);
                    }

                    if (quoteId >= 1)
                    {
                        html = html.Replace("@@Name", entidad.UserEnt.Name);
                        html = html.Replace("@@FirstLastName", entidad.UserEnt.FirstLastName);
                        html = html.Replace("@@Identification", entidad.UserEnt.Identification);
                        html = html.Replace("@@Email", entidad.UserEnt.Email);
                        html = html.Replace("@@PhoneNumber", entidad.UserEnt.PhoneNumber);
                        html = html.Replace("@@Name", entidad.UserEnt.Name);
                        string roomNamesString = string.Join(", ", RoomNames);
                        html = html.Replace("@@RoomNames", roomNamesString);
                        html = html.Replace("@@Details", entidad.QuoteEnt.Details);
                        try
                        {
                            mail.SendMail(entidad.UserEnt.Email, "Cotización de " + entidad.UserEnt.Name + " " + entidad.UserEnt.FirstLastName, html); TempData["notification"] = "Se ha realizado con exito la Cotización.";
                            TempData["notificationType"] = "success";
                            return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                        }
                        catch (Exception)
                        {
                            TempData["notification"] = "No se pudo enviar la información al correo proporcionado";
                            TempData["notificationType"] = "error";
                            return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                        }
                    }
                    else
                    {
                        TempData["notification"] = "Error al realizar la solicitud de Cotización.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al realizar la solicitud de Cotización: " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }
            return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
        }

        [HttpPost]
        public ActionResult AddPropertyDoc(QuoteEnt entidad, int q)
        {
            int result = SaveDocs(q, entidad);
            if (result == 1)
            {
                TempData["notification"] = "Se ha realizado con éxito la Cotización.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
            return View("ViewQuotations", "Quotations");
        }

        public int SaveDocs(int q, QuoteEnt entidad)
        {
            if (entidad.Docs[0] != null)
            {
                QuoteEnt quoteEnt = new QuoteEnt();

                string extension = string.Empty;

                string rutaBase = AppDomain.CurrentDomain.BaseDirectory + "AppContent\\Quotations\\Docs\\Quotations" + q + "\\";

                string ruta = string.Empty;

                quoteEnt.Id = q;

                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                int cantidadArchivos = Directory.GetFiles(rutaBase).Length + 1;

                foreach (var item in entidad.Docs)
                {
                    extension = Path.GetExtension(Path.GetFileName(item.FileName));
                    ruta = rutaBase + item.FileName;
                    item.SaveAs(ruta);
                    quoteEnt.DocUrl = "/AppContent/Quotations/Docs/Quotations" + q + "/" + item.FileName;
                    quoteModel.AddDocUrl(quoteEnt);
                    cantidadArchivos++;
                }
                return 1;
            }
            return -1;
        }

        [HttpGet]
        public ActionResult RegisterQuotation()
        {
            ViewBag.DocumentType = documentType.ListDocumentType();
            QuotationsEnt quotation = new QuotationsEnt();
            var rooms = quoteRoom.GetQuoteRooms().Where(x => x.IsActive == true).ToList();
            quotation.QuoteRoomEnt = rooms;
            return View(quotation);
        }

        [HttpGet]
        [SecurityModel]
        public ActionResult ViewQuotations()
        {
            var datos = quote.ConsultQuote().ToList();
            return View(datos);
        }

        [HttpGet]
        [SecurityModel]
        public ActionResult ChangeQuoteStatus(int q)
        {
            var resp = quoteModel.ChangeQuoteStatus(q);
            if (resp >= 1)
            {
                return RedirectToAction("ViewQuotations", "Quotations");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del usuario";
                return View();
            }
        }

        //public ActionResult AddQuoteDocuments(int q)
        //{
        //    QuoteEnt quoteEnt = new QuoteEnt();
        //    quoteEnt.Id = q;
        //    return View(quoteEnt);
        //}
        public ActionResult DownloadFile(string fileName, int q)
        {
            var rutaArchivo = Path.Combine(Server.MapPath("~/AppContent/Quotations/Docs/Quotations" + q), fileName);

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

        [HttpGet]
        public ActionResult ViewQuoteDocuments(int q)
        {
            QuoteEnt quoteEnt = new QuoteEnt();
            var rutaDirectorio = Server.MapPath("~/AppContent/Quotations/Docs/Quotations" + q);
            ViewBag.QuoteId = q;
            quoteEnt.Id = q;
            if (Directory.Exists(rutaDirectorio))
            {
                var archivos = Directory.GetFiles(rutaDirectorio).Select(Path.GetFileName).ToList();


                quoteEnt.StringList = archivos;
                return View(quoteEnt);
            }

            return View();
        }

        public ActionResult DeleteDoc(int q, string fileName)
        {
            DeleteDocFromPath(q, fileName);
            quoteModel.DeleteDocUrl(q);
            TempData["notification"] = "Se eliminó el documento correctamente.";
            TempData["notificationType"] = "success";
            return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"], property = q });
        }

        public int DeleteDocFromPath(int q, string fileName)
        {
            string rutaBase = AppDomain.CurrentDomain.BaseDirectory + "AppContent\\Quotations\\Docs\\Quotations" + q + "\\";
            var rutaArchivo = Path.Combine(Server.MapPath("~/AppContent/Quotations/Docs/Quotations" + q), fileName);
            if (Directory.Exists(rutaBase))
            {
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                }
                Directory.Delete(rutaBase);
                if (Directory.Exists(rutaBase))
                {
                    return -1;
                }
            }
            return 1;
        }

    }
}
