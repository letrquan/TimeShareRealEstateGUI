using APIDataAccess.DTO.RequestModels;
using APIDataAccess.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace TimeshareUISolution.Pages.Admin
{
    public class SignUpModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPostRegister()
        {
            string fullname = Request.Form["txt_name"];
            string firstname = fullname.Split(' ')[0];
            AccountRequestModel request = new AccountRequestModel()
            {
                FullName = fullname,
                FirstName = firstname,
                LastName = fullname.Substring(fullname.IndexOf(firstname) + firstname.Length).Trim(),
                Email = Request.Form["txt_email"],
                Role = int.Parse(Request.Form["role"]),
                Password = Request.Form["password"],
            };

            string data = JsonConvert.SerializeObject(request);

            HelperFeature.Instance.CallApiAsyncPost("https://localhost:7246/api/Authenticate/SendVerificationCode/" + Request.Form["txt_email"], "", "");

            return RedirectToPage("Verify", new {request = data});
        }
    }
}
