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
    public class MaintenanceModel
    {
        public string UrlApi = ConfigurationManager.AppSettings["UrlApi"];
        SecurityModel SecurityModel = new SecurityModel();

        // Métodos para cambiar el estado de diferentes entidades
        public int ChangeProvinceStatus(ProvinceEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeProvinceStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangeCantonStatus(CantonEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeCantonStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangeDistrictStatus(PropertyAddressEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeDistrictStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangeDocumentTypeStatus(DocumentTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeDocumentTypeStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangeStatusStatus(StatusEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeStatusStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangePropertyTypeStatus(PropertyTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangePropertyTypeStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangeRoleStatus(RoleEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeRoleStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ChangeTransactionTypesStatus(TransactionTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeTransactionTypesStatus?id=";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public RoleEnt GetRoleByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetRoleByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<RoleEnt>().Result;
            }
        }
        public StatusEnt GetStatusByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetStatusByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<StatusEnt>().Result;
            }
        }
        public ProvinceEnt GetProvinceById(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetProvinceById?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<ProvinceEnt>().Result;
            }
        }
        public CantonEnt GetCantonById(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetCantonById?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<CantonEnt>().Result;
            }
        }
        public DistrictEnt GetDistrictByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetDistrictByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<DistrictEnt>().Result;
            }
        }

        public PropertyTypeEnt GetPropertyTypesByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetPropertyTypesByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<PropertyTypeEnt>().Result;
            }
        }
        public TransactionTypeEnt GetTransactionTypesByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetTransactionTypesByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<TransactionTypeEnt>().Result;
            }
        }
        public DocumentTypeEnt GetDocumentTypeByIdSP(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetDocumentTypeByIdSP?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<DocumentTypeEnt>().Result;
            }
        }

        // Métodos para obtener todas las entidades
        public List<TransactionTypeEnt> GetAllTransactionTypes()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetAllTransactionTypes";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<TransactionTypeEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }

        public List<StatusEnt> GetAllStatuses()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetAllStatus";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<StatusEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }
        public List<RoleEnt> GetAllRole()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetAllRole";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<RoleEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }

        public List<PropertyTypeEnt> GetAllPropertyTypes()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetAllPropertyTypes";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<PropertyTypeEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }

        public List<DocumentTypeEnt> GetAllDocumentTypes()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetAllDocumentType";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<DocumentTypeEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }

        public List<ProvinceEnt> GetProvinces()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetProvinces";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<ProvinceEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }

        public List<CantonEnt> GetCantons()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetCantons";
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<CantonEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }

        public List<DistrictEnt> GetDistrictsSP()
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetDistrictsSP";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<DistrictEnt>>().Result;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
        }

        //UpdateRole
        public int UpdateRoleSP(RoleEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateRoleSP";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //UpdateStatus
        public int UpdateStatus(StatusEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateStatus";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //UpdateTransactionType
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

        //UpdatePropertyType
        public int UpdatePropertyType(PropertyTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdatePropertyType";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //UpdateDocumentType
        public int UpdateDocumentType(DocumentTypeEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateDocumentType";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //EditProvinceSP
        public int UpdateProvince(ProvinceEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "UpdateProvince";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //EditCantonSP
        public int EditCantonSP(CantonEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "EditCantonSP";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        //EditCantonSP
        public int EditDistrictSP(DistrictEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "EditDistrictSP";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }


        //Registrar Provincia
        public int RegisterProvince(ProvinceEnt entidad)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterProvince";
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

        //Registrar Canton
        public int RegisterCanton(CantonEnt entidad)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterCanton";
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
        //Registrar Distrito
        public int RegisterDistrict(DistrictEnt entidad)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    string url = UrlApi + "RegisterDistrict";
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


        //Métodos de Rooms
        //Obtiene todos las habitaciones.
        public List<QuoteRoomEnt> GetQuoteRooms()
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "GetQuoteRooms";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<QuoteRoomEnt>>().Result;
            }
        }

        //Obtiene una habitación por Id.
        public QuoteRoomEnt GetRoomById(int q)
        {
            using (var client = new HttpClient())
            {
                var url = UrlApi + "GetRoomById?q=" + q;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<QuoteRoomEnt>().Result;
            }
        }

        //Actualizar el estado de una habitación.
        public int ChangeRoomStatus(QuoteRoomEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = UrlApi + "ChangeRoomStatus";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                JsonContent content = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<int>().Result;
            }
        }

    }
}
