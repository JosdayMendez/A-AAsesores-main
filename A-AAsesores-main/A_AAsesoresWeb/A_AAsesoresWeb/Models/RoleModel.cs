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
    public class RoleModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        //RegisterRole
        public int RegisterRole(RoleEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterRole";
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




        //UpdateRole
        public int UpdateRole(RoleEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateRole";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result; // Cambiar a PostAsync
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }


        //Lista de roles en un dropdownlist
        public List<SelectListItem> ListRole()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListRole";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

    }
}