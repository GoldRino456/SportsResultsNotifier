using System.Net;
using System.Net.Mail;

namespace SportsResultsNotifier;

public static class EmailManager
{
    //SMTP Settings
    static string _smtpAddress = "127.0.0.1";
    static int _portNumber = 25;
    static bool _enableSsl = false;
    static string _emailFromAddress = "No-Reply@RangersSportsReport.com";
    static string _password = "abcde1234";
    static string _destinationAddress = "smtp@none.com";

    public static void SendEmail(EmailData emailData)
    {
        using (MailMessage mail = new())
        {
            mail.From = new MailAddress(_emailFromAddress);
            mail.To.Add(_destinationAddress);

            mail.Subject = emailData.Subject;
            mail.Body = emailData.Body;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new(_smtpAddress, _portNumber))
            {
                smtp.Credentials = new NetworkCredential(_emailFromAddress, _password);
                smtp.EnableSsl = _enableSsl;
                smtp.Send(mail);
            }
        }
    }
}
