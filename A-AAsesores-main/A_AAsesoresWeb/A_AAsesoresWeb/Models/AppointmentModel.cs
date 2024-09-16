using A_AAsesoresWeb.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace A_AAsesoresWeb.Models
{
    public class AppointmentModel
    {
        private readonly string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        public int RegisterAppointment(AppointmentEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "RegisterAppointment";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Get All Appointments
        public List<AppointmentEnt> GetAppointments()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetAppointments";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<AppointmentEnt>>().Result;
            }
        }

        //Change Appointment Status
        public int ChangeAppointmentStatus(AppointmentEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeAppointmentStatus";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Consultar Cita por Id
        public AppointmentEnt ConsultAppointment(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ConsultAppointment?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<AppointmentEnt>().Result;
            }
        }

        //Update Appointment
        public int UpdateAppointment(AppointmentEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateAppointment";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
    }
}