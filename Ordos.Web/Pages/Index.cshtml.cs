using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordos.Web.Pages
{
    public class IndexModel : PageModel
    {
        [Required]
        [BindProperty]
        public string ContactName { get; set; }

        [Required]
        [EmailAddress]
        [BindProperty]
        public string ContactEmail { get; set; }

        [Required]
        [BindProperty]
        public string ContactNumber { get; set; }

        public string SendButtonText { get; set; } = "Enviar";

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            SendButtonText = "Gracias por tu mensaje!";
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(ContactEmail, ContactName);
            var subject = "New Contact From Ordos";
            var to = new EmailAddress("ignacio.delaparra@gmail.com", "Gabriel");
            var plainTextContent = "New Contact";
            var htmlContent = $"<p>New Contact:</p><p>{ContactName}</p><p>{ContactEmail}</p><p>{ContactNumber}</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return RedirectToPage("./GraciasContact");
        }
    }
}
