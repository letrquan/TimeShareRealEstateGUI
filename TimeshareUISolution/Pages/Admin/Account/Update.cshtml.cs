using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace TimeshareUISolution.Pages.Admin.Account
{
    public class UpdateModel : PageModel
    {
        private readonly IAccountService _service;
        public UpdateModel(IAccountService service)
        {
            _service = service;
        }
        public AccountViewModel Account { get; set; }
        public ActionResult OnGet(string accountID)
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
            //if (user.Value.Role != ((int)AccountRole.ADMIN))
            //{
            //    return RedirectToPage("/Admin/Login");
            //}
            var response = _service.GetModelAsync<ResponseResult<AccountViewModel>>(path: $"/GetAccountById/{accountID}", token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if(response.Item1.Value != null)
                {
                    Account = response.Item1.Value;
                }
                else
                {
                    var errorMessage = response.Item1.Message;
                    if (errorMessage != null)
                    {
                        return RedirectToPage("/Admin/Account/Index");
                    }
                }
            }
            else
            {
                var errorMessage = response.Item2;
                if (errorMessage != null)
                {
                    return RedirectToPage("/Admin/Account/Index");
                }
            }
            return Page();
        }
        public IActionResult OnPost(string id,string firstName, string lastName, string email,string phoneNumber, string address, string state, string city, string status, string password, string role)
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
            //if (user.Value.Role != ((int)AccountRole.ADMIN))
            //{
            //    return RedirectToPage("/Admin/Login");
            //}
            var update = new AccountRequestModel
            {
                FirstName = firstName,
                LastName = lastName,
                FullName = $"{firstName} {lastName}",
                City = city,
                Email = email,
                Phone = phoneNumber,
                Address = address,
                State = state,
                Country = Request.Form["country"],
                Status = int.Parse(status),
                Password = password,
                Role = int.Parse(role)
            };
            var response = _service.Put<AccountRequestModel, AccountViewModel>(update ,path: $"/UpdateAccountById/{id}", token: user.AccessToken).Result;
            if (response != null)
            {
                if(response.result == true)
                {
                    TempData["successMessage"] = response.Message;
                    return Redirect($"/Admin/Account/Update?accountID={id}");
                }
                else
                {
                    OnGet(id);
                    TempData["errorMessage"] = response.Message;
                    return Page();
                }
            }
            else
            {
                OnGet(id);
                TempData["errorMessage"] = "Server error";
                return Page();
            }
        }
    }
}
