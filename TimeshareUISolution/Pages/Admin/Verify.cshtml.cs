using APIDataAccess.DTO.RequestModels;
using APIDataAccess.Helpers;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace TimeshareUISolution.Pages.Admin
{
    public class VerifyModel : PageModel
    {
        private static AccountRequestModel AccountRequestModel { get; set; }
        public IActionResult OnGet(string request)
        {

            AccountRequestModel = JsonConvert.DeserializeObject<AccountRequestModel>(request);
            return Page();
        }
        public IActionResult OnPostVerify()
        {
            string opt = String.Concat(Request.Form["number"].ToString().Split(","));

            if(AccountRequestModel.Role == 3)
            {
                AccountRequestModel.Status = (int)AccountStatus.ACTIVE;
            }else
            {
                AccountRequestModel.Status = (int)AccountStatus.PENDING_APPROVAL;

                var test = HelperFeature.Instance.CallApiAsyncPost("https://localhost:7246/api/Notification/SendMailNotification/" + AccountRequestModel.Email + "/" + "Tài khoản đang chờ duyệt!", 
                   "", "Tài khoản đang chờ duyệt, chúng tôi sẽ liên hệ bạn trong thời gian sớm nhất!").Result;
                string s = "s";

            }

            HelperFeature.Instance.CallApiAsyncPost("https://localhost:7246/api/Authenticate/Register/" + opt, "", AccountRequestModel);
            return RedirectToPage("Login");
        }
    }
}
