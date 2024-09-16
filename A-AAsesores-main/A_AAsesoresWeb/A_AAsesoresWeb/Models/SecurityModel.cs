using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace A_AAsesoresWeb.Models
{
    public class SecurityModel : ActionFilterAttribute
    {
        public string SecretKey = ConfigurationManager.AppSettings["SecretKey"];
        private readonly string UserAPI = ConfigurationManager.AppSettings["UserAPI"];
        private readonly string PwdAPI = ConfigurationManager.AppSettings["PwdAPI"];
        public string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string Decrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string GenerateRandomPassword()
        {
            int length = 8;
            const string valid = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Verifica si la sesión con la clave "Login" es nula
            if (HttpContext.Current.Session["Login"] == null)
            {
                // Si es nula, crea un resultado de redirección a la acción "IniciarSesion" en el controlador "Home"
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "Employee" },
                    { "action", "LoginEmployee" }
                });
            }
            // Llama al método base para permitir que la acción continúe ejecutándose si la sesión está presente
            base.OnActionExecuting(filterContext);
        }
        //public string AuthAPI()
        //{
        //    string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(HttpContext.Current.Session["Token"].ToString()));
        //    return base64Credentials;
        //}
    }
}