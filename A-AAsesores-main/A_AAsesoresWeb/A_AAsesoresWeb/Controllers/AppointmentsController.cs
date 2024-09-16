using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace A_AAsesoresWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        //Instancia de los modelos
        readonly AppointmentModel appointment = new AppointmentModel();
        readonly EmployeeModel employee = new EmployeeModel();
        readonly DocumentTypeModel documentType = new DocumentTypeModel();
        readonly UserModel user = new UserModel();
        readonly StatusModel status = new StatusModel();
        readonly SecurityModel security = new SecurityModel();
        readonly Mail mail = new Mail();

        [ActionName("_AddAppointment")]
        [HttpGet]
        public ActionResult CreateAppoinment(string parametro, string parametro2)
        {
            ViewBag.ListDocumentType = documentType.ListDocumentType();
            ViewBag.Parametro = parametro;
            ViewBag.Parametro2 = parametro2;

            return PartialView("_AddAppointment");
        }

        [HttpPost]
        public ActionResult CreateAppointment(MultiEnt entidad)
        {
            //Validar si el usuario ya existe y si no existe se crea
            entidad.UserEnt.Id = user.RegisterUser(entidad.UserEnt);
            //Validar si el usuario ya existe y si no existe se crea
            if (entidad.UserEnt.Id >= 1)
            {
                //Asigno el id del usuario al objeto de la cita en la propiedad UserId
                entidad.AppointmentEnt.UserId = entidad.UserEnt.Id;
                //ejecuto el metodo para registrar la cita y si se registra correctamente envio el correo
                if (appointment.RegisterAppointment(entidad.AppointmentEnt) >= 1)
                {
                    string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_Appointment.html";
                    string html = System.IO.File.ReadAllText(urlHtml);

                    html = html.Replace("@@Name", entidad.UserEnt.Name);
                    html = html.Replace("@@PropertyName", entidad.AppointmentEnt.PropertyName);
                    html = html.Replace("@@AppointmentDate", entidad.AppointmentEnt.AppointmentDate.ToString());
                    mail.SendMail(entidad.UserEnt.Email, "Se ha programado su cita", html);
                    // Redirige solo si todo se ha completado correctamente
                    TempData["notification"] = "Se ha registrado la cita correctamente.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                }
            }
            else if (entidad.UserEnt.Id == -3)
            {
                TempData["notification"] = "El correo electrónico ya se encuentra registrado favor de verificar.";
                TempData["notificationType"] = "error";
            }
            TempData["notification"] = "Ya existe una cita programada para la fecha seleccionada o usted ya tiene una cita programada.";
            TempData["notificationType"] = "error";
            return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult ConsultAppointments()
        {
            var datos = appointment.GetAppointments().ToList();
            if (datos.Count > 0)
            {
                if (Session["Role"].ToString().AsInt() == 1)
                {
                    return View(datos);
                }
                else if (Session["Role"].ToString().AsInt() == 2)
                {
                    if (datos.Where(x => x.EmployeeId == Session["IdTEmployee"].ToString().AsInt()).ToList().Count > 0)
                    {
                        return View(datos.Where(x => x.EmployeeId == Session["IdTEmployee"].ToString().AsInt()).ToList());
                    }
                    else
                    {
                        ViewBag.MensajeUsuario = "No tiene citas asignadas en este momento.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.MensajeUsuario = "No tiene permisos para ver esta información.";
                    return View();
                }
            }
            else
            {
                ViewBag.MensajeUsuario = "No hay citas registradas en el sistema.";
                return View();
            }
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult ChangeAppointmentStatus(int q)
        {
            var entidad = new AppointmentEnt();
            entidad.Id = q;
            var resp = appointment.ChangeAppointmentStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("ConsultAppointments", "Appointments");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado de la cita.";
                return View();
            }
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult UpdateAppointment(int q)
        {
            var datos = appointment.ConsultAppointment(q);
            ViewBag.ListStatus = status.ListAppointmentStatuses();
            ViewBag.EmployeeList = employee.ListEmployees();
            return View(datos);
        }

        [SecurityModel]
        [HttpPost]
        public ActionResult UpdateAppointment(AppointmentEnt entidad)
        {
            if (appointment.UpdateAppointment(entidad) >= 1)
            {

                if (entidad.Status == 10 || entidad.Status == 9)
                {
                    // Si el estado es Confirmada o Cancelada, envía un correo al cliente
                    string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_Appointment_Confirm.html";
                    string html = System.IO.File.ReadAllText(urlHtml);
                    html = html.Replace("@@ClientName", entidad.UserName);
                    html = html.Replace("@@AppointmentDate", entidad.AppointmentDate.ToString());
                    html = html.Replace("@@PropertyName", entidad.PropertyName);

                    string statusText;

                    switch (entidad.Status)
                    {
                        case 9:
                            statusText = "aceptada";
                            break;
                        case 10:
                            statusText = "cancelada";
                            break;
                        default:
                            statusText = "Estado desconocido"; // Opcional: mensaje para valores no esperados
                            break;
                    }

                    html = html.Replace("@@Status", statusText);
                    mail.SendMail(entidad.Email, "Su visita ha a la propiedad " + entidad.PropertyName + " ha sido " + statusText, html);
                }
                // Redirige solo si todo se ha completado correctamente

                TempData["notification"] = "Se ha actualizado la información de la cita correctamente.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });


            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo la cita.";
                ViewBag.DocumentType = documentType.ListDocumentType();
                ViewBag.ListStatus = status.ListAppointmentStatuses();
                TempData["notification"] = "Hubo un error al actualizar la información de la cita.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        [HttpGet]
        public ActionResult ObtenerReservas()
        {
            var resp = appointment.GetAppointments();
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}