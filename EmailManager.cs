using HtmlAgilityPack;
using System.Net;
using System.Net.Mail;

namespace SportsResultsNotifier;

public static class EmailManager
{
    //SMTP Settings
    static string _smtpAddress = "127.0.0.1";
    static int _portNumber = 25;
    static bool _enableSsl = false;
    static string _emailFromAddress = "No-Reply@NbaResults.com";
    static string _password = "abcde1234";
    static string _destinationAddress = "smtp@none.com";

    //Email Template
    static string _templatePath = @"template.html";
    static string _placeholderValue = "#GAMEDATA_INSERT#";

    public static void SendEmail(EmailData emailData)
    {
        using (MailMessage mail = new())
        {
            mail.From = new MailAddress(_emailFromAddress);
            mail.To.Add(_destinationAddress);

            mail.Subject = emailData.Subject;
            mail.Body = FillInEmailTemplate(emailData.Body);
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new(_smtpAddress, _portNumber))
            {
                smtp.Credentials = new NetworkCredential(_emailFromAddress, _password);
                smtp.EnableSsl = _enableSsl;
                smtp.Send(mail);
            }
        }
    }

    private static string FillInEmailTemplate(string body)
    {
        var doc = new HtmlDocument();
        doc.Load(_templatePath);

        return doc.DocumentNode.InnerText.Replace(_placeholderValue, body);
    }
}
