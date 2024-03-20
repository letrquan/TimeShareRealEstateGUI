using APIDataAccess.DTO.ResponseModels.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TimeshareUISolution.Pages.Admin
{
    public class LogoutModel : PageModel
    {
        public UserLoginResponse User { get; set; }

        private readonly ILogger<LoginModel> _logger;
        public LogoutModel(ILogger<LoginModel> logger)
        {
        }

        public ActionResult OnGet()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToPage("/User/Index");
        }

        public ActionResult OnPost(string email, string password)
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToPage("/User/Index");
        }
    }
}
