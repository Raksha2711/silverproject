using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Admin.Web.Helper
{
    public static class ControllerExtensions
    {
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
    public static class PdfHelper
    {
        public static byte[] ConvertToPdf(string html)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                using (var sr = new StringReader(html))
                {
                    htmlparser.Parse(sr);
                }
                pdfDoc.Close();
                return memoryStream.ToArray();
            }
        }
    }
    public static class Email
    {
        public static async Task Send(string to, string subject, string body, bool isHtml = false, string fileName = null, byte[] attachment = null)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    // client.EnableSsl = true;
                    //client.Host = "smtp.gmail.com";
                    //client.Port = 587;
                    client.Host = "smtpout.asia.secureserver.net";
                    client.Port = 80;

                    // setup Smtp authentication
                    NetworkCredential credentials = new NetworkCredential("purchase@slprice.com", "health@159");
                    client.UseDefaultCredentials = true;
                    client.Credentials = credentials;
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("purchase@slprice.com");
                    msg.To.Add(new MailAddress(to));
                    msg.Subject = subject;
                    msg.IsBodyHtml = isHtml;
                    msg.Body = body;
                    //Attachment attachment;
                    if (attachment != null)
                    {
                        Attachment attach = new Attachment(new MemoryStream(attachment), fileName, MediaTypeNames.Application.Pdf);
                        msg.Attachments.Add(attach);
                    }
                    try
                    {
                        client.Send(msg);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
