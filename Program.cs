using SportsResultsNotifier;

var email = new EmailData() { Body = "Body!", Subject = "Subject Line" };
EmailManager.SendEmail(email);