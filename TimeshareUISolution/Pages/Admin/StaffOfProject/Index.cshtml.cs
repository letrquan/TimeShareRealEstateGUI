using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.Admin.StaffOfProject
{
    public class IndexModel : PageModel
    {
        private readonly IStaffOfProjectService _service;
        private readonly IAccountService _accountService;
        private readonly IProjectService _projectService;
        public static int CurrentPage { get; set; }
        public static int TotalPage { get; set; }
        public int PageSize { get; set; } = 3;
        public IndexModel(IStaffOfProjectService service, IAccountService accountService, IProjectService projectService)
        {
            _service = service;
            _accountService = accountService;
            _projectService = projectService;
        }
        public List<StaffOfProjectsViewModel> StaffOfProjectList { get; set; }
        public List<AccountViewModel> StaffList { get; set; }
        public List<AccountViewModel> ProjectList { get; set; }
        public IActionResult OnGet(string? number = null, string? filter = null)
        {
            CurrentPage = int.Parse(number != null ? number : "1");

            CurrentPage = (CurrentPage < 1) ? 1 : CurrentPage;
            CurrentPage = (CurrentPage > TotalPage && TotalPage != 0 ? TotalPage : CurrentPage);
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
            if (user.Value.Role != ((int)AccountRole.ADMIN) && user.Value.Role != ((int)AccountRole.STAFF))
            {
                return RedirectToPage("/Admin/Login");
            }
            var response = _service.GetModelAsync<DynamicModelsResponse<StaffOfProjectsViewModel>>
                (path: $"/GetStaffOfProjects", token: user.AccessToken).Result;

            if (response.Item1 != null)
            {
                if (response.Item1 != null)
                {
                    StaffOfProjectList = response.Item1.Results;
                }
                else
                {
                    var errorMessage = response.Item1.Message;
                    if (errorMessage != null)
                    {
                    }
                }

            }
            var accountListResponse = _accountService.GetModelAsync<DynamicModelsResponse<AccountViewModel>>(path: $"/GetListAccount", token: user.AccessToken).Result;
            if (accountListResponse.Item1 != null)
            {
                StaffList = accountListResponse.Item1.Results;
                StaffList = StaffList.Where(c => c.Role.Equals(2)).ToList();
            } else
            {
                StaffList = new();
            }

            return Page();
        }

        public IActionResult OnPostSearch()
        {
            return OnGet(filter: Request.Form["search"]);
        }
    }
}
