using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.DTO.ResponseModels;
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
        public string Message { get; set; }
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
                HelperFeature.Instance.CallApiAsyncPost("https://localhost:7246/api/Notification/SendMailNotification/" + AccountRequestModel.Email + "/" + "Tài khoản đang chờ duyệt!",
                 "", "Tài khoản đang chờ duyệt, chúng tôi sẽ liên hệ bạn trong thời gian sớm nhất!");
            }

            var data = HelperFeature.Instance.CallApiAsyncPost("https://localhost:7246/api/Authenticate/Register/" + opt, "", AccountRequestModel).Result;

            if (!JsonConvert.DeserializeObject<ResponseResult<AccountViewModel>>(data).Message.Equals("Create Success"))
            {
                return RedirectToPage("SignUp", new { message = "Đăng ký không thành công!" });

            }

            return RedirectToPage("Login");
        }
    }
}
