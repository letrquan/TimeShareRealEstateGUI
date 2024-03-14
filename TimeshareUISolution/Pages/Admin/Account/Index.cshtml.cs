using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.Admin.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _service;
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public List<AccountViewModel> AccountList { get; set; }
        public IndexModel(IAccountService service)
        {
            _service = service;
        }
        public IActionResult OnGet()
        {
            var userStr = HttpContext.Session.GetString("User");
            if (userStr == null || userStr.Count() == 0)
            {
                return RedirectToPage("/Admin/Login");
            }
            var user = JsonConvert.DeserializeObject<UserLoginResponse>(userStr);
            if (user == null)
            {
                return RedirectToPage("/Admin/Login");
            }
            if (user.Value.Role != ((int)AccountRole.ADMIN))
            {
                return RedirectToPage("/Admin/Login");
            }
            var accountRespone = _service.GetModelAsync<DynamicModelsResponse<AccountViewModel>>(path: "/GetListAccount", token: user.AccessToken).Result;
            if (accountRespone.Item1 != null)
            {
                if(accountRespone.Item1.Results != null)
                {
                    AccountList = accountRespone.Item1.Results;
                }
                else
                {
                    var errorMessage = accountRespone.Item1.Message;
                    if (errorMessage != null)
                    {
                    }
                }
                
            }
            else
            {
                var errorMessage = accountRespone.Item2;
                if (errorMessage != null)
                {
                }
            }
            return Page();
        }
        public IActionResult OnPost(string userId)
        {
            var userStr = HttpContext.Session.GetString("User");
            if (userStr == null || userStr.Count() == 0)
            {
                return RedirectToPage("/Admin/Login");
            }
            var user = JsonConvert.DeserializeObject<UserLoginResponse>(userStr);
            if (user == null)
            {
                return RedirectToPage("/Admin/Login");
            }
            if (user.Value.Role != ((int)AccountRole.ADMIN))
            {
                return RedirectToPage("/Admin/Login");
            }
            var response = _service.Delete<AccountViewModel>(path: $"/DeleteAccountById/{userId}", token: user.AccessToken).Result;
            if (response != null)
            {
                if(response.result == true)
                {
                    TempData["successMessage"] = response.Message;
                    return RedirectToPage("/Admin/Account/Index");
                }
                else
                {
                    TempData["errorMessage"] = response.Message;
                    return RedirectToPage("/Admin/Account/Index");
                }
            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return Page();
            }

        }
    }
}
