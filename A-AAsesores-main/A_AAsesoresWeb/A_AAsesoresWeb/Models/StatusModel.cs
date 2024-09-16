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
    public class StatusModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        //RegisterStatus
        public int RegisterStatus(StatusEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterStatus";
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

    //UpdateStatus
    public string UpdateStatus(StatusEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateStatus";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Lista Todos los estados en un dropdown list
        public List<SelectListItem> ListAllStatus()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListStatus";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //Lista los estados de Empleados en un dropdown list
        public List<SelectListItem> ListEmployeeStatuses()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListEmployeeStatuses";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public List<SelectListItem> ListAppointmentStatuses()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListAppointmentStatuses";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //DeleteStatus

    }
}