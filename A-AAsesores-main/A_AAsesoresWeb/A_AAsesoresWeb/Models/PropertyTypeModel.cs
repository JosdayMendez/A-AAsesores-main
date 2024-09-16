using A_AAsesoresWeb.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Models
{
    public class PropertyTypeModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        //Registra un tipo de propiedad
        public int RegisterPropertyType(PropertyTypeEnt entidad)
        {
            try
            {
                    using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterPropertyType";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                    JsonContent contenido = JsonContent.Create(entidad);
                    var resp = client.PostAsync(url, contenido).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<int>().Result;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        //Obtiene todos los tipos
        public List<PropertyTypeEnt> GetPropertiesImgs()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetPropertyTypes";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<PropertyTypeEnt>>().Result;
            }
        }
        //Actualiza un tipo de propiedad
        public int UpdatePropertyType(PropertyTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdatePropertyType";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Elimina un tipo de propiedad
        public int DeletePropertyType(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeletePropertyType?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Se usa en propiedades para listar los tipos de propiedades
        public List<SelectListItem> ListPropertyTypes()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListPropertyTypes";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

    }
}