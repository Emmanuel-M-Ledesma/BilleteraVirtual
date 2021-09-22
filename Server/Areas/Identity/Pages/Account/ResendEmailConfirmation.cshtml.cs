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

using System.Web;
using System.Timers;
using Microsoft.JSInterop;
using ServiceStack;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Threading;

namespace BilleteraVirtual.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        

        Startup obj = new Startup();
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

        private System.Timers.Timer aTimer = new System.Timers.Timer(10800000);
        private int Token = 10800000;
        public int StartTimer(int Tiempo)
        {
            aTimer.Enabled = true;
            Tiempo = CountDownTimer(Convert.ToInt32(aTimer.Interval));
            return Tiempo;
        }

        public int CountDownTimer(int counter)
        {
            if (counter > 0)
            {
                counter -= 1;
            }
            return counter;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (aTimer.Interval == 0)
            {
                int R = StartTimer(Token);

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
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
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return Page();

            }
            else
            {
                CountDownTimer(Convert.ToInt32(aTimer.Interval));
            }
            return Page();
        }
    }
}
