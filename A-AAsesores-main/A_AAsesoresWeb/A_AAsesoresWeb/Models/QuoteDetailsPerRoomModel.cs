using A_AAsesoresWeb.Entities;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace A_AAsesoresWeb.Models
{
    public class QuoteDetailsPerRoomModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        public int RegisterQuoteDetailsPerRoom(QuoteDetailsPerRoomEnt entity)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterQuoteDetailsPerRoom";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entity);
                var response = client.PostAsync(url, content).Result;
                return response.Content.ReadFromJsonAsync<int>().Result;
            }
        }
    }
}