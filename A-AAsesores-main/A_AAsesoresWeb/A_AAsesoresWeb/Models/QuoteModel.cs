using A_AAsesoresWeb.Controllers;
using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class QuoteModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        public int RegisterQuote(QuoteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterQuote";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public List<QuoteResultEnt> ConsultQuote()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ConsultQuote";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<QuoteResultEnt>>().Result;
            }
        }

        public int ChangeQuoteStatus(int? id)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeQuoteStatus?id=" + id.ToString();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(id);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int AddDocUrl(QuoteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "AddDocUrl";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int DeleteDocUrl(int? id)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeleteDocUrl?id=" + id.ToString();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(id);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
    }

}