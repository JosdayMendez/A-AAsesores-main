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
    public class TransactionTypeModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();
        //Registra un tipo de transacción
        public int RegisterTransactionType(TransactionTypeEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterTransactionType";
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
        //Obtiene todos los tipos
        public List<TransactionTypeEnt> GetTransactionTypes()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetTransactionTypes";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<TransactionTypeEnt>>().Result;
            }
        }
        //Actualiza un tipo de transacción
        public int UpdateTransactionType(TransactionTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateTransactionType";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }
        //Elimina un tipo de transacción
        public int DeleteTransactionType(int q)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "DeleteTransactionType?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.DeleteAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //Se usa en propiedades para listar empleados
        public List<SelectListItem> ListTransactionTypes()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "ListTransactionTypes";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

    }
}