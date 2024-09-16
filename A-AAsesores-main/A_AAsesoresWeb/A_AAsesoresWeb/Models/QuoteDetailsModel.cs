using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Models
{
    public class QuoteDetailsModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        public int RegisterQuoteDetails(QuoteDetailsEnt entity)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterQuoteDetails";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entity);
                var response = client.PostAsync(url, content).Result;
                return response.Content.ReadFromJsonAsync<int>().Result;
            }
        }


        public List<SelectListItem> ListQuoteDetails()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListQuoteDetails";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }
    }
}