using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public interface  IEmail
    {
        // bool SendMail(string to, string from, string cc, string body, string subject, string title, bool isCustomer, string fromText, string ApiKey, string attachBody = "", List<string> attachments = null);
        //  bool SendMassMail(string to, string from, string cc, string body, string subject, string title, bool isCustomer, string fromText, string ApiKey, string attachBody = "", List<string> attachments = null);
        // bool SendMail(string to, string from, string cc, string body, string subject, string title, bool isCustomer, string fromText, string ApiKey, string schetime, string attachBody = "", List<string> attachments = null);
        //  int GetReq(string api_key, string api_user, int applimit);
        //  string GetUnsubs(List<string> mails, string api_key, string api_user);
        //  byte[] PdfBytes(List<string> pages);
        Task<bool> SendMail(string to, string from, string cc, string body, string subject);//, string fromText, string apiKey);

    }
}
