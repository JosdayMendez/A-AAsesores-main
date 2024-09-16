using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.Mvc;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class PropertyModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        //Registra un documento
        public int RegisterProperty(PropertyEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterProperty";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Obtiene todos los documentos
        public List<PropertyEnt> GetProperties()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetProperties";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<PropertyEnt>>().Result;
            }
        }
        //Actualiza un documento
        public int UpdateProperty(PropertyEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateProperty";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Elimina un documento
        public int DeleteProperty(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeleteProperty?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Se usa en propiedades para listar empleados
        public List<SelectListItem> ListPropertiesStatuses()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListPropertiesStatuses";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public PropertyEnt GetProperty(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetProperty?q=" + q;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<PropertyEnt>().Result;
            }
        }

        public int ChangePropertyState(PropertyEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangePropertyState";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

    }
}