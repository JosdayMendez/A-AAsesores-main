using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.Mvc;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class PropertyAddressModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        //Registra una dirección
        public int RegisterPropertyAddress(PropertyAddressEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterPropertyAddress";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Obtiene todas las direcciones
        public List<PropertyAddressEnt> GetPropertiesAddresses()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetPropertiesAddresses";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<PropertyAddressEnt>>().Result;
            }
        }
        //Actualiza una dirección
        public int UpdatePropertyAddress(PropertyAddressEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdatePropertyAddress";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Elimina una dirección
        public int DeletePropertyAddress(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeletePropertyAddress?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Se usa en propiedades para listar provincias
        public List<SelectListItem> ListProvinces()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListProvinces";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //Se usa en propiedades para listar cantones
        public List<SelectListItem> ListCantons(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListCantons?q=" + q;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //Se usa en propiedades para listar distritos
        public List<SelectListItem> ListDistricts(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListDistricts?q=" + q;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //Obtener la dirección de una propiedad específica
        public PropertyAddressEnt GetPropertyAddress(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetPropertyAddress?q=" + q;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<PropertyAddressEnt>().Result;
            }
        }

    }
}