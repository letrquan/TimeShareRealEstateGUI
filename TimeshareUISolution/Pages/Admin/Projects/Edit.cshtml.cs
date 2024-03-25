using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace TimeshareUISolution.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly IProjectService _service;

        public EditModel(IProjectService service)
        {
            _service = service;

        }
        public ProjectViewModel Project { get; set; }
        public Dictionary<int, string> Enum { get; set; }
        public Dictionary<int, string> Status { get; set; }
        public IActionResult OnGet(string projectId)
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
            var response = _service.GetModelAsync<ResponseResult<ProjectViewModel>>(path: $"/GetProject/{projectId}", token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1.Value != null)
                {
                    Project = response.Item1.Value;
                    var enumResponse = _service.GetModelAsync<EnumViewModel>(path: $"/GetPriorityTypeProject", token: user.AccessToken).Result;
                    if (enumResponse.Item1 != null)
                    {
                        if (enumResponse.Item1.Results != null)
                        {
                            Enum = enumResponse.Item1.Results;
                        }
                        else
                        {
                            TempData["errorMessage"] = enumResponse.Item1.Message;
                        }
                        var statusResponse = _service.GetModelAsync<EnumViewModel>(path: $"/GetProjectStatus", token: user.AccessToken).Result;
                        if (statusResponse.Item1 != null)
                        {
                            if (statusResponse.Item1.Results != null)
                            {
                                Status = statusResponse.Item1.Results;
                            }
                            else
                            {
                                TempData["errorMessage"] = statusResponse.Item1.Message;
                            }
                        }
                        else
                        {
                            TempData["errorMessage"] = statusResponse.Item1.Message;
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Server error";
                    }
                    return Page();
                }
                else
                {
                    TempData["errorMessage"] = "Server error";
                }
            }
            else
            {
                TempData["errorMessage"] = "Server error";
            }
            return RedirectToPage("/Admin/Projects/Index");
        }

        //onpost 
        public IActionResult OnPost(int projectId, string projectName, int priorityType, string code, int totalSlot, DateTime startDate, DateTime endDate, int status, DateTime openDate, DateTime regisEndDate)
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
            if(endDate <= startDate || openDate >= regisEndDate)
            {
                TempData["errorMessage"] = "Date invalid";
                OnGet(projectId.ToString());
                return Page();
            }
            var update = new ProjectRequestModel
            {
                /*ProjectId = projectId,*/
                ProjectName = projectName,
                PriorityType = priorityType,
                ProjectCode = code,
                TotalSlot = totalSlot,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                RegistrationOpeningDate = openDate,
                RegistrationEndDate = regisEndDate
            };
            var response = _service.Put<ProjectRequestModel, ProjectViewModel>(update, path: $"/UpdateProject/{projectId}", token: user.AccessToken).Result;

            if (response != null)
            {
                if (response.result == true)
                {
                    TempData["successMessage"] = response.Message;
                    return Redirect($"/Admin/Projects/Edit?projectId={projectId}");
                }
                else
                {
                    OnGet(projectId.ToString());
                    TempData["errorMessage"] = response.Message;
                    return Page();
                }
            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return RedirectToPage("/Projects/Index");
            }




        }





    }
}
