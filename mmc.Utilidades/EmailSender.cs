using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

//TEMPLATE DE EMAILS PROVISIONAL
namespace mmc.Utilidades
{
    //hacemos que herede de IEmailSender que se instala y depues implementamos la interface es lo unico que se hace en esta clase
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Enviar(subject, htmlMessage, email);
        }

        public Task Enviar(string subjet, string mensaje, string email)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add(email);
                mm.Subject = subjet;
                mm.Body = mensaje;
                mm.From = new MailAddress("williamgomez686@gmail.com");
                mm.IsBodyHtml = true;

                //Configuracion del servidor
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                string correo = "williamgomez686@gmail.com";
                string passwd = "V3r0n1c42017";
                smtp.Credentials = new System.Net.NetworkCredential(correo, passwd);

                return smtp.SendMailAsync(mm);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }
    }
}
