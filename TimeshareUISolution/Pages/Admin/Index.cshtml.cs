using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Helpers;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Linq;

namespace TimeshareUISolution.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public static DashboardResponse<int> Results { get; set; }   
        public ICollection<ContractViewModel> Contracts { get; set; }
        public Dictionary<string, decimal> DashboardContructionType { get; set; }
        public Dictionary<string, decimal> DashboardProjectName { get; set; }
        public string[] NameContructionType { get; set; }
        public ICollection<float> ValueContructionType { get; set; }
        public IActionResult OnGet()
        {
            UserLoginResponse user = GetUserLogin();
            var data = HelperFeature.Instance.CallApiAsync("https://localhost:7246/api/Dashboard/GetDashboardByMonth", user.AccessToken).Result;

            Results = JsonConvert.DeserializeObject<DashboardResponse<int>>(data);
            return OnGetContracts();

        }

        public IActionResult OnGetContracts()
        {
            UserLoginResponse user = GetUserLogin();
            var data = HelperFeature.Instance.CallApiAsync("https://localhost:7246/api/Contract/GetListContract", user.AccessToken).Result;
            Contracts = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<ContractViewModel>>(data).Results;
            return OnGetDashboardContructType();
        }

        public IActionResult OnGetDashboardContructType()
        {
            UserLoginResponse user = GetUserLogin();
            var data = HelperFeature.Instance.CallApiAsync("https://localhost:7246/api/Dashboard/GetRevenueContructionTypeName", user.AccessToken).Result;
            DashboardContructionType = JsonConvert.DeserializeObject<DashboardResponse<string>>(data).Values;

            NameContructionType = DashboardContructionType.Keys.ToArray();
            var sum = DashboardContructionType.Values.Sum();
            ValueContructionType =  DashboardContructionType.Values.Select(x => (float)Math.Round((float)x / (float)sum * 100, 3) ).ToList();

            return OnGetDashboardProjectName();
        }
        public IActionResult OnGetDashboardProjectName()
        {
            UserLoginResponse user = GetUserLogin();
            var data = HelperFeature.Instance.CallApiAsync("https://localhost:7246/api/Dashboard/GetRevenueProjectName", user.AccessToken).Result;
            DashboardProjectName = JsonConvert.DeserializeObject<DashboardResponse<string>>(data).Values;
            return Page();
        }

        private UserLoginResponse GetUserLogin()
        {
            var userStr = HttpContext.Session.GetString("User");
            if (userStr == null || userStr.Count() == 0)
            {
                RedirectToPageLogin();
            }
            var user = JsonConvert.DeserializeObject<UserLoginResponse>(userStr);
            if (user == null)
            {
                RedirectToPageLogin();
            }
            if (user.Value.Role != ((int)AccountRole.ADMIN) && user.Value.Role != ((int)AccountRole.STAFF))
            {
                RedirectToPageLogin();
            }

            return user;

        }

        private IActionResult RedirectToPageLogin() => RedirectToPage("/Admin/Login");
    }
}
