using A_AAsesoresWeb.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Models
{
    public class CurrencyModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        //Registra una moneda
        public int RegisterCurrency(CurrencyEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterCurrency";
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
        //Obtiene todas las monedas
        public List<CurrencyEnt> GetAllCurrencies()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetAllCurrencies";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<CurrencyEnt>>().Result;
            }
        }
        //Obtiene una moneda por medio del ID
        public CurrencyEnt GetCurrencyByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetCurrencyByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<CurrencyEnt>().Result;
            }
        }

        //Actualiza una moneda
        public int UpdateCurrency(CurrencyEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateCurrency";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Elimina una moneda
        public int DeleteCurrency(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeleteCurrency?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Lista las monedas como un select list item
        public List<SelectListItem> ListCurrencies()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListCurrencies";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                var currencies = res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;

                // Reemplaza el texto si el valor es 1
                foreach (var item in currencies)
                {
                    if (item.Value == "1")
                    {
                        item.Text = "₡"; // Símbolo del colón costarricense
                    }
                }

                return currencies;
            }
        }
    }
}