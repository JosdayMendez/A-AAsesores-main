using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [AllowAnonymous]
        /*Insertar*/
        [HttpPost]
        [Route("RegisterUser")]
        public int RegisterUser(UserEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    // Configura los parámetros para el stored procedure
                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@DocumentType", entidad.DocumentType),
                        new SqlParameter("@Identification", entidad.Identification.Replace("-","")),
                        new SqlParameter("@Name", entidad.Name),
                        new SqlParameter("@FirstLastName", entidad.FirstLastName),
                        new SqlParameter("@SecondLastName", entidad.SecondLastName),
                        new SqlParameter("@PhoneNumber", entidad.PhoneNumber.Replace("-","")),
                        new SqlParameter("@Email", entidad.Email),
                        // El parámetro de salida para el ID generado por el stored procedure
                        new SqlParameter("@ID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        }
                    };
                    // Ejecuta el stored procedure
                    context.Database.ExecuteSqlCommand("exec InsertUserSP @DocumentType, @Identification, @Name, @FirstLastName, @SecondLastName, @PhoneNumber, @Email, @ID OUTPUT", parameters);
                    // Recupera el valor de salida después de ejecutar el stored procedure
                    int generatedID = Convert.ToInt32(parameters[7].Value);
                    return generatedID;
                }
            }
            catch (Exception)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la ejecución del stored procedure
                return -1;
            }
        }

        [AllowAnonymous]
        /*Consultar un usuario en especifico*/
        [HttpGet]
        [Route("GetUserByIdentification")]
        public GetUserByIdentificationSP_Result GetUserByIdentification(string q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetUserByIdentificationSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        //Obtiene solamente los clientes
        [HttpGet]
        [Route("ConsultClients")]
        public List<GetAllClientsSP_Result> ConsultClients()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetAllClientsSP().ToList();
                }
            }
            catch (Exception)
            {
                // Manejo de errores: Puedes registrar el error, lanzar una excepción más específica, etc.
                return new List<GetAllClientsSP_Result>();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ListUser")]
        public List<System.Web.Mvc.SelectListItem> ListUser()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllUsersSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Identification });
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
