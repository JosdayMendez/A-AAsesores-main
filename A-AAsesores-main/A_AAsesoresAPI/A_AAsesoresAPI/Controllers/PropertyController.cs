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
    public class PropertyController : ApiController
    {
        [HttpPost]
        [Route("RegisterProperty")]
        public int RegisterProperty(PropertyEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    // Configura los parámetros para el stored procedure
                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Title", entidad.Title),
                        new SqlParameter("@EmployeeId", entidad.EmployeeId),
                        new SqlParameter("@Currency", entidad.Currency),
                        new SqlParameter("@Price", entidad.Price),
                        new SqlParameter("@AreaLand", entidad.AreaLand),
                        new SqlParameter("@AreaBuild", entidad.AreaBuild),
                        new SqlParameter("@Description", entidad.Description),
                        new SqlParameter("@PropertyType", entidad.PropertyType),
                        new SqlParameter("@TransactionType", entidad.TransactionType),
                        //Esto se quema en el SP por defecto, pero se coloca por si se requiere manejar
                        new SqlParameter("@PropertyStatus", entidad.PropertyStatus),
                        // El parámetro de salida para el ID generado por el stored procedure
                        new SqlParameter("@Id", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        }
                    };
                    // Ejecuta el stored procedure
                    context.Database.ExecuteSqlCommand("exec InsertPropertySP @Title, @EmployeeId, @Currency, @Price, @AreaLand, @AreaBuild, @Description, @PropertyType, @TransactionType, @PropertyStatus, @Id OUTPUT", parameters);
                    // Recupera el valor de salida después de ejecutar el stored procedure
                    int generatedID = Convert.ToInt32(parameters[10].Value);
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
        [HttpGet]
        [Route("GetProperties")]
        public List<GetAllPropertiesSP_Result> GetProperties()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetAllPropertiesSP().ToList();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetProperty")]
        public GetPropertySP_Result GetProperty(int q)
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetPropertySP(q).FirstOrDefault();
            }
        }

        [HttpPut]
        [Route("UpdateProperty")]
        public int UpdateProperty(PropertyEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdatePropertySP(entidad.Id, entidad.Title, entidad.EmployeeId, entidad.Currency, entidad.Price, entidad.AreaLand, entidad.AreaBuild,
                                                    entidad.Description, entidad.PropertyType, entidad.TransactionType, entidad.PropertyStatus);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeleteProperty")]
        public int DeleteProperty(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeletePropertySP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPut]
        [Route("ChangePropertyState")]
        public int ChangePropertyState(PropertyEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangePropertyStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
