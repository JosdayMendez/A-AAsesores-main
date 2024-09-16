using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace A_AAsesoresWeb.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class EmployeeController : Controller
    {
        //Instancia de los modelos
        readonly EmployeeModel employee = new EmployeeModel();
        readonly UserModel user = new UserModel();
        readonly DocumentTypeModel documentType = new DocumentTypeModel();
        readonly StatusModel status = new StatusModel();
        readonly RoleModel role = new RoleModel();
        readonly SecurityModel security = new SecurityModel();
        readonly Mail mail = new Mail();

        //Login de Empleados
        [HttpGet]
        public ActionResult LoginEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginEmployee(EmployeeEnt entidad)
        {
            entidad.Password = security.Encrypt(entidad.Password);
            var resp = employee.LogIn(entidad);
            if (resp != null && resp.IsTemporary != true && resp.Status == 3)
            {
                Session["UserId"] = resp.UserId;
                Session["IdTEmployee"] = resp.IdTEmployee;
                Session["Identification"] = resp.Identification;
                Session["Role"] = resp.Role;
                Session["RoleName"] = resp.RoleName;
                Session["Status"] = resp.Status;
                Session["EmployeeConnect"] = resp.Name;
                Session["EmployeeImage"] = resp.ImageProfile;
                Session["EmployeeEmail"] = resp.Email;
                Session["Token"] = resp.Token;
                HttpContext.Session["Login"] = true;
                TempData["notification"] = "Inicio de sesión exitoso";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
            else if (resp != null && resp.IsTemporary == true && resp.Status == 3)
            {
                Session["EmployeeConnect"] = resp.Name;
                Session["Identification"] = resp.Identification;
                Session["EmployeeEmail"] = resp.Email;
                HttpContext.Session["Login"] = true;
                TempData["notification"] = "La contraseña es temporal, por favor cambie su contraseña.";
                TempData["notificationType"] = "warning";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
            else if (resp != null && resp.Status == 5)
            {
                TempData["notification"] = "Su usuario esta bloqueado, por favor comuniquese con el administrador.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }

            else
            {
                TempData["notification"] = "Compruebe la información de sus credenciales.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        //Olvidé mi contraseña
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(EmployeeEnt entidad)
        {
            var resp = employee.GetEmployee(entidad.Identification.Replace("-", ""));
            //validar que el empleado exista
            if (resp != null)
            {
                entidad.Password = security.Encrypt(security.GenerateRandomPassword());
                entidad.IsTemporary = true;
                string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_Reset_Password.html";
                string html = System.IO.File.ReadAllText(urlHtml);
                html = html.Replace("@@Name", resp.Name);
                html = html.Replace("@@Password", security.Decrypt(entidad.Password));
                //Validar que el correo se envie correctamente
                if (mail.SendMail(resp.Email, "Cambio de contraseña", html))
                {
                    //Si el correo se envia correctamente, entonces se actualiza la contraseña
                    if (employee.ForgetPassword(entidad) != null)
                    {
                        //Redirige solo si todo se ha completado correctamente
                        TempData["notification"] = "Se ha enviado un correo con su nueva contraseña.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                    else
                    { 
                        //Si no se actualiza la contraseña, entonces se muestra un mensaje de error
                        TempData["notification"] = "Hubo un problema para enviar el correo con la nueva contraseña.";
                        TempData["notificationType"] = "error";
                        return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                }
                else
                {
                    //Si no se envia el correo, entonces se muestra un mensaje de error y no se actualiza la contraseña
                    TempData["notification"] = "Hubo un problema para enviar el correo con la nueva contraseña, la contraseña no se ha actualizado.";
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                }
            }
            else
            {
                //Si el empleado no existe, entonces se muestra un mensaje de error
                TempData["notification"] = "El empleado no existe.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        //Cerrar Sesión
        [SecurityModel]
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LoginEmployee", "Employee");
        }

        //Cambiar Contraseña con una contraseña temporal y definir una nueva contraseña
        [HttpGet]
        public ActionResult ResetPassword()
        {
            var entidad = new EmployeeEnt();
            entidad.Identification = Session["Identification"].ToString();
            entidad.Email = Session["EmployeeEmail"].ToString();
            entidad.Name = Session["EmployeeConnect"].ToString();
            return View(entidad);
        }

        [HttpPost]
        public ActionResult ResetPassword(EmployeeEnt entidad)
        {
            if (entidad.Password.Trim() == entidad.PasswordTemp.Trim())
            {
                TempData["notification"] = "La contraseña no puede ser igual a la anterior.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });

            }
            entidad.Password = security.Encrypt(entidad.Password);
            entidad.PasswordTemp = security.Encrypt(entidad.PasswordTemp);
            entidad.IsTemporary = false;
            if (employee.ResetPassword(entidad) >= 1)
            {
                string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_Change_Password.html";
                string html = System.IO.File.ReadAllText(urlHtml);
                html = html.Replace("@@Name", entidad.Name);
                mail.SendMail(entidad.Email, "Se ha relizado el cambio con éxito de su contraseña", html);
                Session.Clear();
                TempData["notification"] = "Se ha cambiado la contraseña con éxito.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
            else
            {
                TempData["notification"] = "Hubo un problema para cambiar la contraseña.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        //Restablecer la contraseña de un empleado por parte del empleado por que quise cambiarla
        [SecurityModel]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var entidad = new EmployeeEnt();
            entidad.Identification = Session["Identification"].ToString();
            return View(entidad);
        }

        [SecurityModel]
        [HttpPost]
        public ActionResult ChangePassword(EmployeeEnt entidad)
        {
            entidad.Password = security.Encrypt(entidad.Password);
            entidad.Email = Session["EmployeeEmail"].ToString();
            if (employee.ChangePassword(entidad) >= 1)
            {
                string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_Change_Password.html";
                string html = System.IO.File.ReadAllText(urlHtml);
                html = html.Replace("@@Name", entidad.Name);
                mail.SendMail(entidad.Email, "Se ha realizado el cambio de contraseña con éxito", html);
                Session.Clear();
                TempData["notification"] = "Se ha cambiado la contraseña con éxito.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
            else
            {
                TempData["notification"] = "Hubo un problema para cambiar la contraseña.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult ProfileEmployee()
        {
            int UserId = int.Parse(Session["UserId"].ToString());
            ViewBag.ListDocumentType = documentType.ListDocumentType();
            ViewBag.ListRole = role.ListRole();
            ViewBag.ListStatus = status.ListEmployeeStatuses();
            var datos = employee.ConsultEmployee(UserId);
            return View(datos);
        }

        [SecurityModel]
        [HttpPost]
        public ActionResult ProfileEmployee(HttpPostedFileBase ImgProfile, EmployeeEnt entidad)
        {
            int respuesta = employee.UpdateEmployee(entidad);
            if (respuesta >= 1)
            {
                if (ImgProfile != null)
                {
                    string extension = Path.GetExtension(Path.GetFileName(ImgProfile.FileName));
                    string rutaBase = AppDomain.CurrentDomain.BaseDirectory + "AppContent\\Users\\Images\\";
                    string ruta = rutaBase + "Prof-" + entidad.Identification.Replace("-", "") + extension;
                    ImgProfile.SaveAs(ruta);
                    entidad.ImageProfile = "/AppContent/Users/Images/Prof-" + entidad.Identification.Replace("-", "") + extension;
                    entidad.UserId = entidad.IdTEmployee;
                    employee.UpdatePathImageEmployee(entidad);
                }
                TempData["notification"] = "Se ha actualizado la información con éxito.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });

            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el usuario del usuario";
                ViewBag.DocumentType = documentType.ListDocumentType();
                ViewBag.ListRole = role.ListRole();
                ViewBag.ListStatus = status.ListEmployeeStatuses();
                TempData["notification"] = "Hubo un problema para actualizar al empleado.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });

            }
        }

        //Register a new Employee
        //[SecurityModel]
        [HttpGet]
        public ActionResult RegisterEmployee()
        {
            //Carga los datos necesarios para el registro de un empleado
            ViewBag.DocumentType = documentType.ListDocumentType();
            ViewBag.ListRole = role.ListRole();
            ViewBag.ListStatus = status.ListEmployeeStatuses();
            return View();
        }

        //[SecurityModel]
        [HttpPost]
        public ActionResult RegisterEmployee(HttpPostedFileBase ImgProfile, MultiEnt entidad)
        {
            try
            {
                entidad.UserEnt.Id = user.RegisterUser(entidad.UserEnt);
                if (entidad.UserEnt.Id >= 1)
                {
                    entidad.EmployeeEnt.UserId = entidad.UserEnt.Id;
                    entidad.EmployeeEnt.PasswordTemp = security.Encrypt(security.GenerateRandomPassword());
                    if (employee.RegisterEmployee(entidad.EmployeeEnt) >= 1)
                    {
                        employee.SaveProfileImg(ImgProfile, entidad, entidad.EmployeeEnt.UserId);

                        string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "/Utilities/EmailTemplates/Format_New_User.html";
                        string html = System.IO.File.ReadAllText(urlHtml);

                        html = html.Replace("@@Name", entidad.UserEnt.Name);
                        html = html.Replace("@@Password", security.Decrypt(entidad.EmployeeEnt.PasswordTemp));
                        mail.SendMail(entidad.UserEnt.Email, "Registro de Usuario", html);
                        // Redirige solo si todo se ha completado correctamente
                        TempData["notification"] = "Se ha registrado el empleado con éxito, se le ha enviado un correo con su contraseña temporal.";
                        TempData["notificationType"] = "success";

                        return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    }
                }
                else if (entidad.UserEnt.Id == -3)
                {
                    TempData["notification"] = "El correo electrónico ya se encuentra registrado.";
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });

                }
                // Si hay un problema, establece el mensaje de error y devuelve la vista
                ViewBag.DocumentType = documentType.ListDocumentType();
                ViewBag.ListRole = role.ListRole();
                ViewBag.ListStatus = status.ListEmployeeStatuses();
                TempData["notification"] = "El empleado ya se encuentra registrado.";
                TempData["notificationType"] = "error";
            }
            catch (Exception ex)
            {
                TempData["notification"] = "Error al registrar empleado: " + ex.Message;
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
        }


        //Actualizar un usuario
        [SecurityModel]
        [HttpGet]
        public ActionResult UpdateEmployee(int q)
        {
            var datos = employee.ConsultEmployee(q);
            ViewBag.ListDocumentType = documentType.ListDocumentType();
            ViewBag.ListRole = role.ListRole();
            ViewBag.ListStatus = status.ListEmployeeStatuses();
            return View(datos);
        }

        //Actualizar un usuario
        [SecurityModel]
        [HttpPost]
        public ActionResult UpdateEmployee(HttpPostedFileBase ImgProfile, EmployeeEnt entidad)
        {
            if (employee.UpdateEmployee(entidad) >= 1)
            {
                if (ImgProfile != null)
                {
                    string extension = Path.GetExtension(Path.GetFileName(ImgProfile.FileName));
                    string rutaBase = AppDomain.CurrentDomain.BaseDirectory + "AppContent\\Users\\Images\\";
                    string ruta = rutaBase + "Prof-" + entidad.Identification.Replace("-", "") + extension;
                    ImgProfile.SaveAs(ruta);
                    entidad.ImageProfile = "/AppContent/Users/Images/Prof-" + entidad.Identification.Replace("-", "") + extension;
                    entidad.UserId = entidad.IdTEmployee;
                    employee.UpdatePathImageEmployee(entidad);
                    Session["EmployeeImage"] = entidad.ImageProfile;
                }
                // Redirige solo si todo se ha completado correctamente
                TempData["notification"] = "Se ha actualizado la información del empleado con éxito.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar la información del empleado";
                ViewBag.DocumentType = documentType.ListDocumentType();
                ViewBag.ListRole = role.ListRole();
                ViewBag.ListStatus = status.ListEmployeeStatuses();
                TempData["notification"] = "Hubo un problema para actualizar al empleado.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        //Consultar Todos los Empleados
        [SecurityModel]
        [HttpGet]
        public ActionResult ConsultEmployees()
        {
            int UserId = int.Parse(Session["UserId"].ToString());
            var datos = employee.ConsultEmployees().Where(x => x.UserId != UserId).ToList();
            return View(datos);
        }

        //Consultar Empleado por Id
        [SecurityModel]
        [HttpGet]
        public ActionResult ChangeEmployeeStatus(int q)
        {
            var entidad = new EmployeeEnt();
            entidad.UserId = q;
            var resp = employee.ChangeEmployeeStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("ConsultEmployees", "Employee");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del usuario";
                return View();
            }
        }

    }
}