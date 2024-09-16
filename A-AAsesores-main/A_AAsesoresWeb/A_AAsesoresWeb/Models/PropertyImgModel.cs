using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class PropertyImgModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        //Registra una imagen de propiedad
        public int RegisterPropertyImg(PropertyImgEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterPropertyImg";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Obtiene todas las imagenes
        public List<PropertyImgEnt> GetPropertiesImgs()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetPropertiesImgs";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<PropertyImgEnt>>().Result;
            }
        }
        //Actualiza un documento
        public int UpdatePropertyImg(PropertyImgEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdatePropertyImg";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Elimina una imagen específica
        public int DeletePropertyImg(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeletePropertyImg?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Obtener las imágenes de una propiedad específica
        public List<PropertyImgEnt> GetPropertyImgs(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetPropertyImgs?q=" + q;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<PropertyImgEnt>>().Result;
            }
        }
    }
}