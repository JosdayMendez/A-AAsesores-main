using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Controllers
{
    public class NewsController : AlertsController
    {
        readonly NewsModel newsModel = new NewsModel();
        readonly DocumentTypeModel documentType = new DocumentTypeModel();
        readonly UserModel user = new UserModel();
        readonly Mail mail = new Mail();
        readonly EmployeeModel employee = new EmployeeModel();
        readonly ContactModel contact = new ContactModel();

        string FileRoute = "/AppContent/News/Images/";

        public ActionResult Queries()
        {
            ViewBag.DocumentType = documentType.ListDocumentType();
            return View();
        }

        [HttpGet]
        public ActionResult ListContacts()
        {
            var list = contact.ListContacts().ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult ChangeContactStatus(int q)
        {
            var resp = contact.ChangeContactStatus(q);
            if (resp >= 1)
            {
                return RedirectToAction("ListContacts", "News");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado de la solicitud de contacto";
                return View();
            }
        }

        [HttpPost]
        public ActionResult RegisterQuerie(MultiEnt entidad)
        {
            ContactEnt contactEnt = new ContactEnt();
            string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_Queries.html";
            string html = System.IO.File.ReadAllText(urlHtml);
            string patronCorreo = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.']\w+)*$";
            ViewBag.User = user.ListUser();
            ViewBag.Employee = employee.ListVendorAndAdminEmails();
            if (ModelState.IsValid)
            {
                try
                {
                    int ClientID = 0;
                    bool userFound = false;

                    foreach (var item in ViewBag.User)
                    {
                        if (item.Text == entidad.UserEnt.Identification.Replace("-", ""))
                        {
                            ClientID = Convert.ToInt32(item.Value);
                            userFound = true;
                            break;
                        }
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
                            else
                            {
                                TempData["notification"] = "Por favor ingrese un correo electronico válido";
                                TempData["notificationType"] = "error";
                                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                            }
                        }
                    }
                    contactEnt.UserId = ClientID;
                    contactEnt.Message = entidad.ContactEnt.Message;
                    int result = contact.RegisterContact(contactEnt);
                    if (ClientID >= 1 && result >= 1)
                    {
                        html = html.Replace("@@Name", entidad.UserEnt.Name);
                        html = html.Replace("@@FirstLastName", entidad.UserEnt.FirstLastName);
                        html = html.Replace("@@Identification", entidad.UserEnt.Identification);
                        html = html.Replace("@@Email", entidad.UserEnt.Email);
                        html = html.Replace("@@PhoneNumber", entidad.UserEnt.PhoneNumber);
                        mail.SendMail(entidad.UserEnt.Email, "Solicitud de contacto de " + entidad.UserEnt.Name + " " + entidad.UserEnt.FirstLastName, html);

                        foreach (var item in ViewBag.Employee)
                        {
                            string email = item.Text;
                            mail.SendMail(email, "Solicitud de contacto de " + entidad.UserEnt.Name + " " + entidad.UserEnt.FirstLastName, html);
                        }

                        TempData["notification"] = "Se ha enviado la solicitud de contacto con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al realizar la solicitud de contacto: " + ex.Message;
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

        [HttpGet]
        public ActionResult InformativeBlog()
        {
            var newsList = newsModel.ListNews();

            if (!newsList.Any())
            {
                ViewBag.NoDataMessage = "No hay Noticias disponibles.";
                return View();
            }

            return View(newsList);
        }

        [HttpGet]
        public ActionResult InformativeBlogAdmin()
        {
            int employeeId;

            if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            {
                employeeId = (int)Session["IdTEmployee"];
            }
            else
            {
                return RedirectToAction("LoginEmployee", "Employee");
            }

            Session["IdTEmployee"] = employeeId;
            var newsList = newsModel.ListNews();

            if (!newsList.Any())
            {
                ViewBag.NoDataMessage = "No hay Noticias disponibles.";
            }

            return View(newsList);
        }

        [HttpGet]
        public ActionResult AddInformativeBlog()
        {
            int employeeId;

            if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            {
                employeeId = (int)Session["IdTEmployee"];
            }
            else
            {
                return RedirectToAction("LoginEmployee", "Employee");
            }

            Session["IdTEmployee"] = employeeId;

            NewsEnt newsEnt = new NewsEnt { EmployeeId = employeeId };
            TempData.Clear();
            return View(newsEnt);
        }

        [HttpPost]
        public ActionResult AddInformativeBlog(NewsEnt entidad, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Title) && !string.IsNullOrWhiteSpace(entidad.Description) && imageFile != null && imageFile.ContentLength > 0)
            {
                try
                {
                    // Define the path where the image will be saved
                    string imagePath = Server.MapPath("~" + FileRoute);
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    // Create a unique file name
                    string imageFileName = Path.GetFileName(imageFile.FileName);
                    string path = Path.Combine(imagePath, imageFileName);
                    imageFile.SaveAs(path);

                    // Set the URL of the image
                    entidad.ImageUrl = $"{FileRoute}{imageFileName}";

                    // Save the news entry
                    int idNews = newsModel.RegisterNews(entidad);

                    if (idNews >= 1)
                    {
                        TempData["notification"] = "La noticia se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                    else
                    {
                        TempData["notification"] = "No se pudo registrar la noticia. Inténtelo nuevamente.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar la noticia: " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                // Log validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["notification"] = "Error al registrar la noticia. " + string.Join(", ", errors);
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
        }



        [HttpGet]
        public ActionResult UpdateInformativeBlog(int id)
        {
            int employeeId;

            if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            {
                employeeId = (int)Session["IdTEmployee"];
            }
            else
            {
                return RedirectToAction("LoginEmployee", "Employee");
            }

            Session["IdTEmployee"] = employeeId;

            var data = newsModel.GetNewsByIdSP(id);

            if (data != null)
            {
                return View(data);
            }

            TempData["notification"] = "No se encontró la noticia.";
            TempData["notificationType"] = "error";
            return RedirectToAction("FrequentQuestionsAdmin");
        }

        [HttpPost]
        public ActionResult UpdateInformativeBlog(NewsEnt entidad, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Si no se ha subido una nueva imagen, mantener la URL de la imagen existente
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        // Define the path where the image will be saved
                        string imagePath = Server.MapPath("~" + FileRoute);
                        if (!Directory.Exists(imagePath))
                        {
                            Directory.CreateDirectory(imagePath);
                        }

                        // Create a unique file name
                        string imageFileName = Path.GetFileName(imageFile.FileName);
                        string path = Path.Combine(imagePath, imageFileName);
                        imageFile.SaveAs(path);

                        // Set the URL of the image
                        entidad.ImageUrl = $"{FileRoute}{imageFileName}";
                    }
                    else
                    {
                        // Mantener la URL existente si no se sube una nueva imagen
                        var existingNews = newsModel.GetNewsByIdSP(entidad.Id);
                        if (existingNews != null)
                        {
                            entidad.ImageUrl = existingNews.ImageUrl;
                        }
                    }

                    // Update the news entry
                    int result = newsModel.UpdateNews(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha actualizado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al actualizar la Noticia.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar la Noticia: " + ex.Message;
                    TempData["notificationType"] = "error";
                    TempData["exceptionDetails"] = ex.ToString(); // Store detailed exception info for debugging
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
        }


        [HttpGet]
        public ActionResult UpdateNewsImage(int id)
        {
            int employeeId;

            if (Session["IdTEmployee"] != null && Session["IdTEmployee"] is int)
            {
                employeeId = (int)Session["IdTEmployee"];
            }
            else
            {
                return RedirectToAction("LoginEmployee", "Employee");
            }

            Session["IdTEmployee"] = employeeId;

            var data = newsModel.GetNewsByIdSP(id);

            if (data != null)
            {
                return View(data);
            }

            TempData["notification"] = "No se encontró la noticia.";
            TempData["notificationType"] = "error";
            return RedirectToAction("FrequentQuestionsAdmin");
        }

        [HttpPost]
        public JsonResult UpdateNewsImage(int Id, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~" + FileRoute), fileName);

                try
                {
                    ImageFile.SaveAs(filePath);

                    // Crear una instancia de NewsEnt y actualizar la URL de la imagen
                    NewsEnt news = new NewsEnt
                    {
                        Id = Id,
                        ImageUrl = "~" + FileRoute + fileName
                    };

                    // Llamar al método para actualizar solo la imagen en la base de datos
                    int result = newsModel.UpdateNewsImg(news);

                    if (result > 0)
                    {
                        return Json(new { success = true, message = "Imagen actualizada correctamente." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error al actualizar la imagen en la base de datos." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error al guardar la imagen: " + ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "No se seleccionó ninguna imagen." });
            }
        }



        [HttpPost]
        public ActionResult DeleteInformativeBlog(int id)
        {
            var news = newsModel.GetNewsByIdSP(id);

            if (news != null)
            {

                if (!string.IsNullOrEmpty(news.ImageUrl))
                {
                    // Define el path donde se guarda la imagen
                    string imagePath = Server.MapPath("~" + FileRoute);
                    string imageFilePath = Path.Combine(imagePath, Path.GetFileName(news.ImageUrl));

                    if (System.IO.File.Exists(imageFilePath))
                    {
                        try
                        {
                            System.IO.File.Delete(imageFilePath);
                        }
                        catch (Exception ex)
                        {
                            TempData["notification"] = "Error al eliminar la imagen de la noticia: " + ex.Message;
                            TempData["notificationType"] = "error";
                            return RedirectToAction("FrequentQuestionsAdmin");
                        }
                    }
                    else
                    {
                        TempData["notification"] = "La imagen de la noticia no se encontró en el servidor.";
                        TempData["notificationType"] = "warning";
                    }
                }
                else
                {
                    TempData["notification"] = "La URL de la imagen es nula o vacía.";
                    TempData["notificationType"] = "warning";
                }

                bool result = newsModel.DeleteNewsById(id);

                if (result)
                {
                    TempData["notification"] = "Se ha eliminado la noticia con éxito.";
                    TempData["notificationType"] = "success";
                }
                else
                {
                    TempData["notification"] = "Error al eliminar la noticia.";
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "No se encontró la noticia.";
                TempData["notificationType"] = "error";
            }

            return RedirectToAction("InformativeBlogAdmin");
        }


    }
}
