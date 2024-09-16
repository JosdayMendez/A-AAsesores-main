using A_AAsesoresWeb.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class NewsModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        // RegisterNews
        public int RegisterNews(NewsEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterNews";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        // UpdateNews
        public int UpdateNews(NewsEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateNews";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        // UpdateNewsImg
        public int UpdateNewsImg(NewsEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateNewsImg";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public NewsEnt GetNewsByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetNewsByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<NewsEnt>().Result;
            }
        }

        public List<NewsEnt> ListNews()
        {
            List<NewsEnt> newsList = new List<NewsEnt>();

            try
            {
                using (var client = new HttpClient())
                {
                    string url = UrlApi + "ListNews";
                    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                    var resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<NewsEnt>>().Result;
                    }
                    else
                    {
                        Console.WriteLine("Error: " + resp.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener noticias: " + ex.Message);
            }

            return newsList;
        }

        public bool DeleteNewsById(int id)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeleteNews/" + id;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
