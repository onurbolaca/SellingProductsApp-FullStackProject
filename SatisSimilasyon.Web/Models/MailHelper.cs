using SatisSimilasyon.Entity.Context;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SatisSimilasyon.Web.Models
{
  public static class MailHelper
  {
    public static string SendMail(string message, string nameSurname, string subTitle, string mailAdress)
    {
      var type = string.Empty;

      try
      {
        if (message == null || nameSurname == null || subTitle == null || mailAdress == null)
        {
          type = "0";
        }
        else
        {
          DataContext db = new DataContext();
          var mailParams = db.MailParams.FirstOrDefault();

          var senderMail = new MailAddress(mailParams.Email, "TEST");
          var receiverMail = new MailAddress(mailAdress, nameSurname);
          var pass = mailParams.Password;
          var sub = subTitle;
          var mes = message;
          var smtp = new SmtpClient
          {
            Host = mailParams.SMTP,
            Port = mailParams.Port,
            EnableSsl = mailParams.SSL,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(senderMail.Address, pass)
          };

          using (var mess = new MailMessage(senderMail, receiverMail)
          {
            Subject = sub,
            Body = mes
          })
          {
            smtp.Send(mess);
          }
          type = "1";
        }
      }
      catch (Exception ex)
      {
        type = "2";
      }
      return type;
    }
  }
}