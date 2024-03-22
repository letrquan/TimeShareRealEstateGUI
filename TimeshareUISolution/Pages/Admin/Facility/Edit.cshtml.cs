using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.Admin.Facility
{
    public class EditModel : PageModel
    {
        private readonly IFacilityService _service;
        private readonly IDepartmentService _departmentService;
        public EditModel(IFacilityService service, IDepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }
        public FacilityViewModel Facility { get; set; }
        public DepartmentViewModel Department { get; set; }

        public List <DepartmentViewModel> DepartmentList { get; set; } = new List <DepartmentViewModel> ();
        public Dictionary<int, string> Enum { get; set; }
        public Dictionary<int, string> facilityType { get; set; }
        public IActionResult OnGet(string facilityId)
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
            var response = _service.GetModelAsync<ResponseResult<FacilityViewModel>>(path: $"/GetFacility/{facilityId}", token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1.Value != null)
                {
                    Facility = response.Item1.Value;
                    var enumResponse = _service.GetModelAsync<EnumViewModel>(path: $"/GetFacilityType", token: user.AccessToken).Result;
                    if (enumResponse.Item1 != null)
                    {
                        if (enumResponse.Item1.Results != null)
                        {
                            Enum = enumResponse.Item1.Results;
                        }
                        else
                        {
                            TempData["errorMessage"] = enumResponse.Item1.Message;
                            return RedirectToPage("/Admin/Facility/Index");
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Server error";
                        return RedirectToPage("/Admin/Facility/Index");
                    }
                    var departmentResponse = _departmentService.GetModelAsync<ResponseResult<DepartmentViewModel>>(path: $"/GetDepartment/{Facility.DepartmentId.ToString()}", token: user.AccessToken).Result;
                    if (departmentResponse.Item1 != null)
                    {
                        if (departmentResponse.Item1.Value != null)
                        {
                            Department = departmentResponse.Item1.Value;
                        }
                        else
                        {
                            TempData["errorMessage"] = departmentResponse.Item1.Message;
                            return RedirectToPage("/Admin/Facility/Index");
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Server error";
                        return RedirectToPage("/Admin/Facility/Index");
                    }
                    var departmentListResponse = _departmentService.GetModelAsync<DynamicModelsResponse<DepartmentViewModel>>(path: $"/GetListDepartment", token: user.AccessToken).Result;
                    if (departmentListResponse.Item1 != null)
                    {
                        if (departmentResponse.Item1.Value != null)
                        {
                            DepartmentList = departmentListResponse.Item1.Results; 
                        }
                        else
                        {
                            TempData["errorMessage"] = departmentListResponse.Item1.Message;
                            return RedirectToPage("/Admin/Facility/Index");
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Server error";
                        return RedirectToPage("/Admin/Facility/Index");
                    }
                    return Page();
                }
                else
                {
                    TempData["errorMessage"] = response.Item1.Message;
                    return RedirectToPage("/Admin/Facility/Index");
                }

            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return Page();
            }
        }
        public IActionResult OnPost(int facilityId, string facilityName, string description, int facilityType, int departmentId, string image)
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
            var update = new FacilityRequestModel
            {
                FacilityId = facilityId,
                FacilityName = facilityName,
                Description = description,
                FacilityType = int.Parse(s: Request.Form["FacilityType"]),
                DepartmentId = int.Parse(s: Request.Form["departmentId"]),
                Image = image,

            };
            var response = _service.Put<FacilityRequestModel, FacilityViewModel>(update, path: $"/UpdateFacility/{facilityId}", token: user.AccessToken).Result;
            if (response != null)
            {
                if (response.result == true)
                {
                    TempData["successMessage"] = response.Message;
                    return Redirect($"/Admin/Facility/Edit?FacilityId={facilityId}");
                }
                else
                {
                    OnGet(facilityId.ToString());
                    TempData["errorMessage"] = response.Message;
                    return Page();
                }
            }
            else
            {
                OnGet(facilityId.ToString());
                TempData["errorMessage"] = "Server error";
                return Page();
            }
        }

    }
}
