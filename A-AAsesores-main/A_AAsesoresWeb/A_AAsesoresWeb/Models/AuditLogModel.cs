using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class AuditLogModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        public int RegisterAudit(AuditLogEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterAudit";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Obtienela Auditoria
        public List<AuditLogEnt> GetAudit()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetAudit";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<AuditLogEnt>>().Result;
            }
        }
        //Actualiza la auditoria
        public int UpdateAudit(AuditLogEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateAudit";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Elimina la Auditoria
        public int DeleteAudit(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeleteAudit?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

    }
}