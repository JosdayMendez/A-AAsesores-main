using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using A_AAsesoresAPI.JWT;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class EmployeeController : ApiController
    {
        Security Security = new Security();

        /*Insertar*/
        [HttpPost]
        [Route("RegisterEmployee")]
        public int RegisterEmployee(EmployeeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    int res = context.InsertEmployeeSP(entidad.UserId, entidad.PasswordTemp, entidad.Role);
                    return res;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //Actualiza la imagen de perfil del empleado
        [HttpPut]
        [Route("UpdatePathImageEmployee")]
        public int UpdatePathImageEmployee(Employee entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.UpdateEmployeeImgSP(entidad.UserId, entidad.ImageProfile);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /*Consultar un usuario en especifico*/
        [HttpGet]
        [Route("ConsultEmployee")]
        public GetEmployeeSP_Result ConsultEmployee(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetEmployeeSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        /*Consultar todos los empleados*/
        [HttpGet]
        [Route("ConsultEmployees")]
        public List<GetAllEmployeesSP_Result> ConsultEmployees()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetAllEmployeesSP().ToList();
                }
            }
            catch (Exception)
            {
                // Manejo de errores: Puedes registrar el error, lanzar una excepción más específica, etc.
                return new List<GetAllEmployeesSP_Result>();
            }
        }

        /*Obtiene los empleados en formato de Dropdown */
        [HttpGet]
        [Route("ListEmployee")]
        public List<System.Web.Mvc.SelectListItem> ListEmployee()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = context.GetAllEmployeesSP().ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    respuesta.Add(new System.Web.Mvc.SelectListItem { Value = "", Text = "Por favor seleccione" });
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.IdTEmployee.ToString(), Text = item.FullName });
                    }

                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        //Cambiar el estado de un empleado de Activo a Inactivo y viceversa
        [HttpPut]
        [Route("ChangeEmployeeStatus")]
        public int ChangeEmployeeStatus(EmployeeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeEmployeeStatusSP(entidad.UserId);
                    return 1;


                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        //Actualiza una persona
        [HttpPut]
        [Route("UpdateEmployee")]
        public int UpdateEmployee(EmployeeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.UpdateEmployeeSP(entidad.IdTEmployee, entidad.PhoneNumber.Replace("-", ""), entidad.Email, entidad.Role, entidad.Status);
                    return 1;
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }

        [AllowAnonymous]
        //Iniciar sesión
        [HttpPost]
        [Route("LogIn")]
        public LoginEmployeeSP_Result LogIn(EmployeeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var data = context.LoginEmployeeSP(entidad.Identification.Replace("-", ""), entidad.Password).FirstOrDefault();
                    if (data != null)
                    {
                        data.Token = Security.GenerateToken(entidad.Identification);
                        return data;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [AllowAnonymous]
        //Obtener un empleado por su identificación para recuperar la contraseña
        [HttpGet]
        [Route("GetEmployee")]
        public GetEmployeeByIdentificationSP_Result GetEmployeeByIdentificationSP(string q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetEmployeeByIdentificationSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [AllowAnonymous]
        //Olvide mi contraseña necesito recuperarla
        [HttpPut]
        [Route("ForgetPassword")]
        public ForgetPasswordSP_Result ForgetPassword(EmployeeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.ForgetPasswordSP(entidad.Identification.Replace("-", ""), entidad.Password, entidad.IsTemporary).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [AllowAnonymous]
        //El usuario desea restablecer su contraseña con la contraseña temporal y colocar una nueva contraseña
        [HttpPut]
        [Route("ResetPassword")]
        public int ResetPassword(EmployeeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.ResetPasswordSP(entidad.Identification.Replace("-", ""), entidad.Password, entidad.PasswordTemp, entidad.IsTemporary);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //Restablecer la contraseña de un empleado por parte del empleado
        [HttpPut]
        [Route("ChangePassword")]
        public int ChangePassword(EmployeeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.ChangePasswordSP(entidad.Identification.Replace("-", ""), entidad.Password);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetEmployeeEmailByRoleAndStatus")]
        public List<System.Web.Mvc.SelectListItem> ListEmployeeEmail()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = context.GetEmployeeInfoByRoleAndStatus().ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.EmployeeEmail.ToString(), Text = item.EmployeeEmail });
                    }

                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetVendorAndAdminEmails")]
        public List<System.Web.Mvc.SelectListItem> ListVendorsAndAdminsEmail()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = context.GetVendorAndAdminEmails().ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.EmployeeEmail.ToString(), Text = item.EmployeeEmail });
                    }

                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

    } //Cierre de la clase
} //Cierre del namespace