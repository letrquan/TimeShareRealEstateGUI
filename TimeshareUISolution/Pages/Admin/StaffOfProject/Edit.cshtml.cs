using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.Admin.StaffOfProject
{
    public class EditModel : PageModel
    {
        private readonly IStaffOfProjectService _service;
        private readonly IAccountService _accountService;
        private readonly IProjectService _projectService;
        public static int CurrentPage { get; set; }
        public static int TotalPage { get; set; }
        public int PageSize { get; set; } = 3;
        public EditModel(IStaffOfProjectService service, IAccountService accountService, IProjectService projectService)
        {
            _service = service;
            _accountService = accountService;
            _projectService = projectService;
        }
        public List<StaffOfProjectsViewModel> StaffOfProjectList { get; set; }
        public List<AccountViewModel> StaffList { get; set; }
        public List<ProjectViewModel> ProjectList { get; set; }

        public StaffOfProjectRequestModel Staff {  get; set; }
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
            if (user.Value.Role != ((int)AccountRole.ADMIN) && user.Value.Role != ((int)AccountRole.STAFF))
            {
                return RedirectToPage("/Admin/Login");
            }
            var accountListResponse = _accountService.GetModelAsync<DynamicModelsResponse<AccountViewModel>>(path: $"/GetListAccount", token: user.AccessToken).Result;
            if (accountListResponse.Item1 != null)
            {
                StaffList = accountListResponse.Item1.Results;
                StaffList = StaffList.Where(c => c.Role.Equals(2)).ToList();
            }
            else
            {
                StaffList = new();
            }
            var projectListResponse = _projectService.GetModelAsync<DynamicModelsResponse<ProjectViewModel>>(path: $"/GetListProject", token: user.AccessToken).Result;
            if (projectListResponse.Item1 != null)
            {
                ProjectList = projectListResponse.Item1.Results;
            }
            else
            {
                ProjectList = new();
            }
                return Page();
        }
    }
}
