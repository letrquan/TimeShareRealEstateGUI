using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TimeshareUISolution.Pages.Admin.AvaibleTime
{
    public class IndexModel : PageModel
    {
        private readonly IAvableTimeService _avableTimeService;
        public IndexModel(IAvableTimeService avableTimeService)
        {
            _avableTimeService = avableTimeService;
        }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        public static int CurrentPage { get; set; }
        public static int TotalPage { get; set; }
        public int PageSize { get; set; } = 6;
        public List<AvailableTimeViewModel> AvaiableList { get; set; }
        public void OnGet(int departmentId, string? number = null, string? filter = null)
        {
            CurrentPage = int.Parse(number != null ? number : "1");

            CurrentPage = (CurrentPage < 1) ? 1 : CurrentPage;
            CurrentPage = (CurrentPage > TotalPage && TotalPage != 0 ? TotalPage : CurrentPage);
            var userStr = HttpContext.Session.GetString("User");
            if (userStr == null || userStr.Count() == 0)
            {
                RedirectToPage("/Admin/Login");
            }
            var user = JsonConvert.DeserializeObject<UserLoginResponse>(userStr);
            if (user == null)
            {
                RedirectToPage("/Admin/Login");
            }
            if (user.Value.Role != ((int)AccountRole.ADMIN) && user.Value.Role != ((int)AccountRole.STAFF))
            {
                RedirectToPage("/Admin/Login");
            }
            var response = _avableTimeService.GetModelAsync<DynamicModelsResponse<AvailableTimeViewModel>>(path: $"/GetListAvailableTime?DepartmentId={departmentId}" + filter + "&" + "page=" + CurrentPage + "&pageSize=" + PageSize, token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1 != null)
                {
                    AvaiableList = response.Item1.Results;
                    CurrentPage = response.Item1.Metadata.Page;
                }
                else
                {
                    var errorMessage = response.Item1.Message;
                    if (errorMessage != null)
                    {
                        RedirectToPage("/Admin/Department/Index");
                    }
                }
            }
            else
            {
                var errorMessage = response.Item2;
                if (errorMessage != null)
                {
                    RedirectToPage("/Admin/Department/Index");
                }
            }
        }
        public IActionResult OnPostDelete(int atId, int departmentId)
        {
            var userStr = HttpContext.Session.GetString("User");
            if (userStr == null || userStr.Count() == 0)
            {
                RedirectToPage("/Admin/Login");
            }
            var user = JsonConvert.DeserializeObject<UserLoginResponse>(userStr);
            if (user == null)
            {
                RedirectToPage("/Admin/Login");
            }
            if (user.Value.Role != ((int)AccountRole.ADMIN) && user.Value.Role != ((int)AccountRole.STAFF))
            {
                RedirectToPage("/Admin/Login");
            }
            var response = _avableTimeService.Delete<ResponseResult<AvailableTimeViewModel>>(path: $"/DeleteAvailableTime/{atId}", token: user.AccessToken).Result;
            if (response != null)
            {
                if(response.result == true)
                {
                    TempData["SuccessMessage"] = response.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Server error";
                
            }
            OnGet(departmentId);
            return Page();
        }
        public IActionResult OnPostCreate(int departmentId)
        {
            var userStr = HttpContext.Session.GetString("User");
            if (userStr == null || userStr.Count() == 0)
            {
                RedirectToPage("/Admin/Login");
            }
            var user = JsonConvert.DeserializeObject<UserLoginResponse>(userStr);
            if (user == null)
            {
                RedirectToPage("/Admin/Login");
            }
            if (user.Value.Role != ((int)AccountRole.ADMIN) && user.Value.Role != ((int)AccountRole.STAFF))
            {
                RedirectToPage("/Admin/Login");
            }
            var creatAvaibleTime = new AvailableTimeRequestModel
            {
                StartDate = StartDate,
                EndDate = EndDate,
                Status = (int?)AvailableStatus.AVAILABLE,
                DepartmentProjectCode = null
            };
            var create = _avableTimeService.PostWithResponse<ResponseResult<AvailableTimeViewModel>, AvailableTimeRequestModel>(creatAvaibleTime , path: $"/CreateAvailableTime", token: user.AccessToken).Result;
            if (create.Item1 != null)
            {
                if (create.Item1.result == true)
                {
                    TempData["SuccessMessage"] = create.Item1.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = create.Item1.Message;
                }
            }
            else
            {
                if(create.Item2 != null)
                {
                    TempData["ErrorMessage"] = create.Item2.Title;
                }
                else
                {
                    TempData["ErrorMessage"] = "Server error";
                }
            }
            OnGet(departmentId);
            return Page();
        }
    }
}
