using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TEMPLATE DE EMAILS PROVISIONAL
namespace mmc.Utilidades
{
    //hacemos que herede de IEmailSender que se instala y despues implementamos la interface es lo unico que se hace en esta clase
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
