using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
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
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reestablecer contraseña",
                    $"<table style='width:100%;'><tr style='width:100%;'><td style='text-align:left;width:100%;' colspan='3'><img style='width:150px;height:150px; src='image.png' alt='' /></td></tr>" +
                    $"</tr><tr style = 'width:100%;'><td><font size ='3'>Estimado/a</font><br><br></td></tr><tr style = 'width:100%;'><td><font size = '2'> Nos comunicamos con usted para informarle que se ha solicitado cambiar la contraseña de su cuenta</font><br><br></td></tr>" +
                    $"</table><table style='width:100%;'>" +
                    $"Puede cambiarla haciendo <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click aqui</a>." + "<br><br></td> " + "</tr>" + "</table><table style='width:100%;'>" +
                    $"<td><font size='2'>En caso de no haber solicitado un cambio le pedimos que ignore este mensaje</font>" +
                    $"<tr><td><br/><br/><font size='1' color='#000000'>E-mail generado automaticamente, por favor no responder este correo. </font></td></tr>" + "<tr><td><font size='1' color='#000000'>Powered by: CryptoByte http://www.cryptobyte.com/ </font></td></tr>" + "</table>");


                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
