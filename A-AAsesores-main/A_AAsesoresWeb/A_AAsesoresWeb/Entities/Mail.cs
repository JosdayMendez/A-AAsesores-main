using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace A_AAsesoresWeb.Entities
{
    public class Mail
    {
        public string sendMail(string to, string asunto, string body)
        {
            string msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
            string from = "jus_2402@outlook.com";
            string displayName = "A&A Asesores Inmobiliarios";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(to);

                mail.Subject = asunto;
                mail.Body = body;
                mail.IsBodyHtml = true;


                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, "ibrayn24");
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false


                client.Send(mail);
                msge = "¡Correo enviado exitosamente! Pronto te contactaremos.";

            }
            catch (Exception ex)
            {
                msge = ex.Message + ". Por favor verifica tu conexión a internet y que tus datos sean correctos e intenta nuevamente.";
            }

            return msge;
        }

        public bool SendMail(string to, string asunto, string body)
        {
            try
            {
                string correoSMTP = ConfigurationManager.AppSettings["CuentaCorreo"];
                string claveSMTP = ConfigurationManager.AppSettings["ClaveCorreo"];
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(to));
                msg.From = new MailAddress(correoSMTP, "A&A Asesores Inmobiliarios");
                msg.Subject = asunto;
                msg.Body = body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(correoSMTP, claveSMTP);
                client.Port = 587;
                client.Host = ConfigurationManager.AppSettings["ServidorCorreo"];
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Send(msg);
                // El correo se envió correctamente
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes registrarlos, mostrar un mensaje, etc.
                // Aquí simplemente imprimimos el error en la consola
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
                // Indica que el envío de correo no fue exitoso
                return false;
            }
        }

    }
}