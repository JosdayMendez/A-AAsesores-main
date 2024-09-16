using A_AAsesoresWeb.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class ContactModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        public int RegisterContact(ContactEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterContact";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public List<ContactResultEnt> ListContacts()
        {
            List<ContactResultEnt> contactList = new List<ContactResultEnt>();

            try
            {
                using (var client = new HttpClient())
                {
                    string url = UrlApi + "ListContacts";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                    var resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<ContactResultEnt>>().Result;
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

            return contactList;
        }

        public int ChangeContactStatus(int? id)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeContactStatus?id=" + id.ToString();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(id);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
    }
}