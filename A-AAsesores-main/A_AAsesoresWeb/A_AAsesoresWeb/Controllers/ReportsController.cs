using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using System.Globalization;
using System;
using System.Linq;
using System.Web.Mvc;
using PropertyModel = A_AAsesoresWeb.Models.PropertyModel;
using System.Collections.Generic;

namespace A_AAsesoresWeb.Controllers
{
    public class ReportsController : Controller
    {
        PropertyModel propertyModel = new PropertyModel();
        PropertyAddressModel propertyAddressModel = new PropertyAddressModel();
        EmployeeModel employeeModel = new EmployeeModel();
        UserModel userModel = new UserModel();
        QuoteModel quoteModel = new QuoteModel();
        AppointmentModel appointmentModel = new AppointmentModel();

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PropertiesByPrice()
        {
            try
            {

                var data = propertyModel.GetProperties().Select(item => new
                {
                    Title = item.Title,
                    Price = item.Price
                });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PropertiesByProvince()
        {
            try
            {
                var data = propertyModel.GetProperties()
                    .GroupBy(item => propertyAddressModel.GetPropertyAddress(item.Id).Province)
                    .Select(group => new
                    {
                        Province = group.Key,
                        PropertyCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PropertiesByStatus()
        {
            try
            {
                var data = propertyModel.GetProperties()
                    .GroupBy(item => item.PropertyStatusName)
                    .Select(group => new
                    {
                        Status = group.Key,
                        PropertyCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PropertiesByCreationMonthOnPresentYear()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var cultureInfo = new CultureInfo("es-ES");

                // Obtener todas las propiedades creadas en el año actual
                var properties = propertyModel.GetProperties()
                    .Where(item => item.CreationDate.Year == currentYear)
                    .ToList();

                // Inicializar un diccionario para almacenar el recuento de propiedades por mes
                var propertyCountByMonth = new Dictionary<int, int>();
                for (int month = 1; month <= 12; month++)
                {
                    propertyCountByMonth[month] = 0; // Inicializar cada mes con un recuento de 0
                }

                // Contar las propiedades por mes
                foreach (var property in properties)
                {
                    var month = property.CreationDate.Month;
                    propertyCountByMonth[month]++;
                }

                // Crear una lista de objetos anónimos que contengan el nombre del mes y el recuento de propiedades
                var data = new List<object>();
                var monthNames = cultureInfo.DateTimeFormat.MonthNames;
                for (int month = 0; month < 12; month++)
                {
                    data.Add(new
                    {
                        Month = monthNames[month],
                        PropertyCount = propertyCountByMonth[month + 1] // El índice de los meses comienza en 1
                    });
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult EarningsByMonthOnPresentYear()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var cultureInfo = new CultureInfo("es-ES");

                // Obtener todas las propiedades creadas en el año actual
                var properties = propertyModel.GetProperties()
                    .Where(item => item.SoldDate?.Year == currentYear)
                    .ToList();

                // Inicializar un diccionario para almacenar las ganancias mensuales
                var monthlyEarnings = new Dictionary<int, decimal>();
                for (int month = 1; month <= 12; month++)
                {
                    monthlyEarnings[month] = 0; // Inicializar cada mes con ganancias de 0
                }

                // Contar las propiedades por mes
                foreach (var property in properties)
                {
                    var month = property.SoldDate.HasValue ? property.SoldDate.Value.Month : 0;
                    monthlyEarnings[month] += property.Price;
                }

                // Crear una lista de objetos anónimos que contengan el nombre del mes y el recuento de propiedades
                var data = new List<object>();
                var monthNames = cultureInfo.DateTimeFormat.MonthNames;
                for (int month = 0; month < 12; month++)
                {
                    data.Add(new
                    {
                        Month = monthNames[month],
                        MonthEarnings = monthlyEarnings[month + 1], // El índice de los meses comienza en 1
                    });
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PropertyByType()
        {
            try
            {
                var data = propertyModel.GetProperties()
                    .GroupBy(item => item.PropertyTypeName)
                    .Select(group => new
                    {
                        Type = group.Key,
                        PropertyCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PropertiesByTransaction()
        {
            try
            {
                var data = propertyModel.GetProperties()
                    .GroupBy(item => item.TransactionTypeName)
                    .Select(group => new
                    {
                        Transaction = group.Key,
                        PropertyCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PropertiesByEmployee()
        {
            try
            {
                var data = propertyModel.GetProperties()
                    .GroupBy(item => item.EmployeeName)
                    .Select(group => new
                    {
                        Employee = group.Key,
                        PropertyCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ClientsByMonth()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var cultureInfo = new CultureInfo("es-ES");

                // Obtener todas las propiedades creadas en el año actual
                var clients = userModel.ConsultClients()
                    .Where(item => item.CreationDate.Year == currentYear)
                    .ToList();

                // Inicializar un diccionario para almacenar el recuento de propiedades por mes
                var clientsCountByMonth = new Dictionary<int, int>();
                for (int month = 1; month <= 12; month++)
                {
                    clientsCountByMonth[month] = 0; // Inicializar cada mes con un recuento de 0
                }

                // Contar las propiedades por mes
                foreach (var client in clients)
                {
                    var month = client.CreationDate.Month;
                    clientsCountByMonth[month]++;
                }

                // Crear una lista de objetos anónimos que contengan el nombre del mes y el recuento de propiedades
                var data = new List<object>();
                var monthNames = cultureInfo.DateTimeFormat.MonthNames;
                for (int month = 0; month < 12; month++)
                {
                    data.Add(new
                    {
                        Month = monthNames[month],
                        ClientCount = clientsCountByMonth[month + 1] // El índice de los meses comienza en 1
                    });
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult EmployeesByStatus()
        {
            try
            {
                var data = employeeModel.ConsultEmployees()
                    .GroupBy(item => item.StatusName)
                    .Select(group => new
                    {
                        Status = group.Key,
                        EmployeeCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult EmployeesByRole()
        {
            try
            {
                var data = employeeModel.ConsultEmployees()
                    .GroupBy(item => item.RoleName)
                    .Select(group => new
                    {
                        Roles = group.Key,
                        EmployeeCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult QuotesByMonth()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var cultureInfo = new CultureInfo("es-ES");

                // Obtener todas las propiedades creadas en el año actual
                var quotes = quoteModel.ConsultQuote()
                    .Where(item => item.CreationDate.Year == currentYear)
                    .ToList();

                // Inicializar un diccionario para almacenar el recuento de propiedades por mes
                var quotesCountByMonth = new Dictionary<int, int>();
                for (int month = 1; month <= 12; month++)
                {
                    quotesCountByMonth[month] = 0; // Inicializar cada mes con un recuento de 0
                }

                // Contar las propiedades por mes
                foreach (var quote in quotes)
                {
                    var month = quote.CreationDate.Month;
                    quotesCountByMonth[month]++;
                }

                // Crear una lista de objetos anónimos que contengan el nombre del mes y el recuento de propiedades
                var data = new List<object>();
                var monthNames = cultureInfo.DateTimeFormat.MonthNames;
                for (int month = 0; month < 12; month++)
                {
                    data.Add(new
                    {
                        Month = monthNames[month],
                        QuoteCount = quotesCountByMonth[month + 1] // El índice de los meses comienza en 1
                    });
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult QuotesByStatus()
        {
            try
            {
                var data = quoteModel.ConsultQuote()
                    .GroupBy(item => item.StatusDescription)
                    .Select(group => new
                    {
                        Status = group.Key,
                        QuoteCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PendingAppoitmentsByEmployee()
        {
            try
            {
                var data = appointmentModel.GetAppointments()
                    .Where(item => item.Status == 8)
                    .GroupBy(item => item.EmployeeName ?? "Sin asignar")
                    .Select(group => new
                    {
                        Employee = group.Key,
                        AppointmentCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AppointmentsByProperty()
        {
            try
            {
                var data = appointmentModel.GetAppointments()
                    .GroupBy(item => item.PropertyName)
                    .Select(group => new
                    {
                        Title = group.Key,
                        AppointmentCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AppointmentsByCreationAndAttention()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var cultureInfo = new CultureInfo("es-ES");

                // Obtener todas las propiedades creadas en el año actual
                var createdAppointments = appointmentModel.GetAppointments()
                    .Where(item => item.CreationDate.Year == currentYear)
                    .ToList();
                var attentdedAppointments = appointmentModel.GetAppointments()
                    .Where(item => item.AppointmentDate.Year == currentYear)
                    .ToList();

                // Inicializar un diccionario para almacenar las ganancias mensuales
                var monthlyCreateds = new Dictionary<int, int>();
                for (int month = 1; month <= 12; month++)
                {
                    monthlyCreateds[month] = 0; // Inicializar cada mes con ganancias de 0
                }

                var monthlyAttendeds = new Dictionary<int, int>();
                for (int month = 1; month <= 12; month++)
                {
                    monthlyAttendeds[month] = 0; // Inicializar cada mes con ganancias de 0
                }

                // Contar las propiedades por mes
                foreach (var appointment in createdAppointments)
                {
                    var month = appointment.CreationDate.Month;
                    monthlyCreateds[month]++;
                }

                foreach (var appointment in attentdedAppointments)
                {
                    var month = appointment.AppointmentDate.Month;
                    monthlyAttendeds[month]++;
                }

                // Crear una lista de objetos anónimos que contengan el nombre del mes y el recuento de propiedades
                var data = new List<object>();
                var monthNames = cultureInfo.DateTimeFormat.MonthNames;
                for (int month = 0; month < 12; month++)
                {
                    data.Add(new
                    {
                        Month = monthNames[month],
                        MonthCreateds = monthlyCreateds[month + 1], // El índice de los meses comienza en 1
                        MonthAttendeds = monthlyAttendeds[month + 1], // El índice de los meses comienza en 1
                    });
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AppointmentsByStatus()
        {
            try
            {
                var data = appointmentModel.GetAppointments()
                    .GroupBy(item => item.StatusName)
                    .Select(group => new
                    {
                        Status = group.Key,
                        AppointmentCount = group.Count()
                    });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}