using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class PropertyDocModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        //Registra un documento
        public int RegisterPropertyDoc(PropertyDocEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterPropertyDoc";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Obtiene todos los documentos
        public List<PropertyDocEnt> GetPropertiesDocs()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetPropertiesDocs";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<PropertyDocEnt>>().Result;
            }
        }
        //Actualiza un documento
        public int UpdatePropertyDoc(PropertyDocEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdatePropertyDoc";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Elimina un documento
        public int DeletePropertyDoc(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeletePropertyDoc?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Obtener los documentos de una propiedad específica
        public List<PropertyDocEnt> GetPropertyDocs(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetPropertyDocs?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<PropertyDocEnt>>().Result;
            }
        }
    }
}