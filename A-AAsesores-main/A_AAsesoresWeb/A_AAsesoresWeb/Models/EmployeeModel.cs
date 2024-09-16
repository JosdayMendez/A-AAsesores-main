using A_AAsesoresWeb.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Models
{
    public class EmployeeModel
    {
        private readonly string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        //Register Employee
        public int RegisterEmployee(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                //Estas 3 líneas son las necesarias para consumir un servicio web
                string url = UrlApi + "RegisterEmployee";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                //Aqui se retorna el objeto que se obtiene del servicio web,
                //se convierte a un objeto de tipo string, que es lo que retorna el API
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Consultar Todos los Empleados
        public List<EmployeeEnt> ConsultEmployees()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ConsultEmployees";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<EmployeeEnt>>().Result;
            }
        }

        //Consultar Empleado por Id
        public EmployeeEnt ConsultEmployee(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ConsultEmployee?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<EmployeeEnt>().Result;
            }
        }
        //Se usa en propiedades para listar empleados
        public List<SelectListItem> ListEmployees()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListEmployee";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //ActualizarRutaImagen Profile employee
        public int UpdatePathImageEmployee(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdatePathImageEmployee";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Metodo para guardar la imagen del empleado
        public void SaveProfileImg(HttpPostedFileBase ImgProfile, MultiEnt entidad, int IdEmployee)
        {
            if (ImgProfile != null)
            {
                //Toma la extensión de la imagen
                string extension = Path.GetExtension(Path.GetFileName(ImgProfile.FileName));
                //Ruta donde se guardará la imagen
                string rutaBase = AppDomain.CurrentDomain.BaseDirectory + "AppContent\\Users\\Images\\";
                //Si la ruta no existe, la crea
                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }
                //Ruta completa de la imagen
                string ruta = rutaBase + "Prof-" + entidad.UserEnt.Identification.Replace("-", "") + extension;
                //Guarda la imagen en la ruta especificada
                ImgProfile.SaveAs(ruta);
                //Asigna la ruta de la imagen al objeto
                entidad.EmployeeEnt.ImageProfile = "/AppContent/Users/Images/Prof-" + entidad.UserEnt.Identification.Replace("-", "") + extension;
                //Asigna el Id del usuario al empleado
                entidad.EmployeeEnt.UserId = IdEmployee;
                //Actualiza la ruta de la imagen en la base de datos
                UpdatePathImageEmployee(entidad.EmployeeEnt);
            }
            else
            {
                //Si no se selecciona una imagen, se asigna una por defecto
                entidad.EmployeeEnt.ImageProfile = "/AppContent/Users/Images/ProfileDefault.png";
                //Asigna el Id del usuario al empleado
                entidad.EmployeeEnt.UserId = IdEmployee;
                //Actualiza la ruta de la imagen en la base de datos
                UpdatePathImageEmployee(entidad.EmployeeEnt);
            }

        }

        //Cambia el estado de un usuario de Activo a Inactivo y Viceversa
        public int ChangeEmployeeStatus(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeEmployeeStatus";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Update Employee
        public int UpdateEmployee(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateEmployee";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public EmployeeEnt LogIn(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                //Estas 3 lineas son las necesarias para consumir un servicio web
                string url = UrlApi + "LogIn";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                //Aqui se retorna el objeto que se obtiene del servicio web,
                //se convierte a un objeto de tipo UsuarioEnt, que es lo que retorna el API
                return resp.Content.ReadFromJsonAsync<EmployeeEnt>().Result;
            }
        }

        public List<SelectListItem> ListEmployeeEmails()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetEmployeeEmailByRoleAndStatus";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public List<SelectListItem> ListVendorAndAdminEmails()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetVendorAndAdminEmails";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }
        public EmployeeEnt GetEmployee(string q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetEmployee?q="+ q;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<EmployeeEnt>().Result;
            }
        }


        public EmployeeEnt ForgetPassword(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ForgetPassword";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<EmployeeEnt>().Result;
            }
        }

        public int ResetPassword(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ResetPassword";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangePassword(EmployeeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangePassword";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }


    }
}