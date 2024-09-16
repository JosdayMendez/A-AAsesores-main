using A_AAsesoresWeb.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.Mvc;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class DocumentTypeModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        //ListDocumentType
        public List<SelectListItem> ListDocumentType()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ListDocumentType";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //RegisterDocumentType
        public int RegisterDocumentType(DocumentTypeEnt entidad)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterDocumentType";
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
        //UpdateDocumentType
        public string UpdateDocumentType(DocumentTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateDocumentType";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        //DeleteDocumentType



    }
}