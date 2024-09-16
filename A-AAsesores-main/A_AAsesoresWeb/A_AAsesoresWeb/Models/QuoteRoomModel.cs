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
    public class QuoteRoomModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        public List<QuoteRoomEnt> GetQuoteRooms()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetQuoteRooms";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<QuoteRoomEnt>>().Result;
            }
        }

        public List<SelectListItem> ListQuoteRooms()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListQuoteRooms";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //Registrar una habitación
        public int RegisterRoom(QuoteRoomEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterQuoteRoom";
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

        //Actualizar una habitación
        public int UpdateQuoteRoom(QuoteRoomEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateQuoteRoom";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
    }
}