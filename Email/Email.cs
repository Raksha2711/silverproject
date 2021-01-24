using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Email
{
    //public class Email : IEmail
    //{
    //    //public int GetReq(string api_key, string api_user, int applimit)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public string GetUnsubs(List<string> mails, string api_key, string api_user)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public byte[] PdfBytes(List<string> pages)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public bool SendMail(string to, string from, string cc, string body, string subject, string title, bool isCustomer, string fromText, string ApiKey, string attachBody = "", List<string> attachments = null)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public bool SendMail(string to, string from, string cc, string body, string subject, string title, bool isCustomer, string fromText, string ApiKey, string schetime, string attachBody = "", List<string> attachments = null)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}


    //    //public bool SendMassMail(string to, string from, string cc, string body, string subject, string title, bool isCustomer, string fromText, string ApiKey, string attachBody = "", List<string> attachments = null)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    static async Task<Response> ExecuteMail(string to, string from, string cc, string body, string subject)//, string fromText, string apiKey)
    //    {
    //        try
    //        {
    //            var client = new SendGridClient(apiKey);
    //            var msg = new SendGridMessage()
    //            {
    //                From = new EmailAddress(from, fromText),
    //                Subject = subject,
    //                HtmlContent = body,
    //            };
    //            msg.AddTo(new EmailAddress(to));
    //            var response = await client.SendEmailAsync(msg);
    //            return response;
    //        }

    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public async Task<bool> SendMail(string to, string from, string cc, string body, string subject)//, string fromText, string apiKey)
    //    {
    //        try
    //        {
    //            var response = await ExecuteMail(to, from, cc, body, subject, fromText, apiKey);
    //            if (response.StatusCode.ToString().Equals("Accepted"))
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }
    //}
}
