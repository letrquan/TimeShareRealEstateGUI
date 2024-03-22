using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TimeshareUISolution.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _service;
        private readonly IAuthenticateService _authenticateService;
        public string? ErrorMessage { get; set; }

        public LoginModel(IAccountService service, IAuthenticateService authenticateService)
        {
            _service = service;
            _authenticateService = authenticateService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Không được bỏ trống email")]
        [StringLength(maximumLength: 100, ErrorMessage = "Không được vướt quá 100 kí tự")]
        public string Email { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
        [StringLength(maximumLength: 100, ErrorMessage = "Không được vướt quá 100 kí tự")]
        public string Password { get; set; }
        public void OnGet()
        {
            //call api to get the user details

        }

        public ActionResult OnPostNormalLogin()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UserLoginResponse account = new UserLoginResponse();
            var response = _authenticateService.PostWithResponse<UserLoginResponse>(path: $"/Login/{Email}/{Password}").Result;
            if (response.Item1 != null)
            {
                if(response.Item1.Value != null)
                {
                    account = response.Item1;
                }
                else
                {
                    TempData["ErrorMessage"] = response.Item1.Message;
                    return Page();
                }
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(account));
                HttpContext.Session.SetString("UserRole", account.Value.Role.ToString());
                switch (account.Value.Role)
                {
                    case 0:
                        return RedirectToPage("/User/Index");
                    case 1:
                        return RedirectToPage("/Admin/Index");
                    case 3:
                        return RedirectToPage("/User/Index");
                    default:
                        return RedirectToPage("/User/Index");
                }
            }
            else
            {
                if (response.Item2 == null)
                {
                    return Redirect($"/Error/500");
                }
                TempData["ErrorMessage"] = response.Item2.Title;
                return Page();
            }
        }
    }
}
