using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using BilleteraVirtual.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace BilleteraVirtual.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ResendEmailConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email de verificacion enviado. Por favor varifique su cuenta de email.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Confirme su Email",
                $"<table style='width:100%;'><tr style='width:100%;'><td style='text-align:left;width:100%;' colspan='3'><img style='width:150px;height:150px; src='image.png' alt='' /></td></tr>" +
                $"</tr><tr style = 'width:100%;'><td><font size ='3'>Estimado/a</font><br><br></td></tr><tr style = 'width:100%;'><td><font size = '2'> Nos comunicamos con usted para informarle que su cuenta ha sido creada </font><br><br></td></tr>"+
                $"</table><table style='width:100%;'>" +
                $"Porfavor confirme su cuenta haciendo <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click aqui</a>."+
                $"<tr><td><br/><br/><font size='1' color='#000000'>E-mail generado automaticamente, por favor no responder este correo. </font></td></tr>" + "<tr><td><font size='1' color='#000000'>Powered by: CryptoByte http://www.cryptobyte.com/ </font></td></tr>" + "</table>");

            ModelState.AddModelError(string.Empty, "Email de verificacion enviado. Por favor varifique su cuenta de email.");
            return Page();
        }
    }
}
