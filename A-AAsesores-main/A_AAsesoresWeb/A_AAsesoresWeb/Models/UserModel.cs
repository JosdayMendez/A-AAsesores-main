using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Models
{
    public class UserModel
    {
        private string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        //Register Employee
        public int RegisterUser(UserEnt entidad)
        {
            using (var client = new HttpClient())
            {
                //Estas 3 lineas son las necesarias para consumir un servicio web
                string url = UrlApi + "/RegisterUser";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                //Aqui se retorna el objeto que se obtiene del servicio web,
                //se convierte a un objeto de tipo string, que es lo que retorna el API
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public UserEnt GetUserByIdentification(string q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetUserByIdentification?q=" + q;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<UserEnt>().Result;
            }
        }

        //Consultar únicamente los clientes
        public List<UserEnt> ConsultClients()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ConsultClients";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<UserEnt>>().Result;
            }
        }

        //UpdateUser
        //DeleteUser
        //ListUser
        public List<SelectListItem> ListUser()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListUser";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }
    }
}