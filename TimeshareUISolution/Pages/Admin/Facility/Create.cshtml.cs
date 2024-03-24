using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.Implements;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;
using static System.Net.Mime.MediaTypeNames;

namespace TimeshareUISolution.Pages.Admin.Facility
{
    public class CreateModel : PageModel
    {
        private readonly IFacilityService _service;
        private readonly IDepartmentService _departmentService;
        public CreateModel(IFacilityService service, IDepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }
        public FacilityViewModel Facility { get; set; }
        public DepartmentViewModel Department { get; set; }

        public List<DepartmentViewModel> DepartmentList { get; set; } = new List<DepartmentViewModel>();
        public Dictionary<int, string> Enum { get; set; }
        public Dictionary<int, string> facilityType { get; set; }
        public IActionResult OnGet()
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
            var departmentListResponse = _departmentService.GetModelAsync<DynamicModelsResponse<DepartmentViewModel>>(path: $"/GetListDepartment", token: user.AccessToken).Result;
            if (departmentListResponse.Item1 != null)
            {
                DepartmentList = departmentListResponse.Item1.Results;
            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return RedirectToPage("/Admin/Facility/Index");
            }
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
                Enum = new();
            }
            return Page();
        }
        public IActionResult OnPost(int facilityId, string facilityName, string image, string description, int facilityType, int departmentId)
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
            var create = new FacilityRequestModel
            {
                FacilityId = facilityId,
                FacilityName = facilityName,
                Image = image,
                Description = description,
                FacilityType = facilityType,
                DepartmentId = departmentId,

            };
            var response = _service.PostWithResponse<ResponseResult<FacilityViewModel>, FacilityRequestModel>(create, path: "/CreateFacility", user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1.Value != null)
                {
                    TempData["successMessage"] = response.Item1.Message;
                    return RedirectToPage("/Admin/Facility/Index");
                }
                else
                {
                    TempData["errorMessage"] = response.Item1.Message;
                    OnGet();
                    return Page();
                }
            }
            else
            {
                TempData["errorMessage"] = "Server error";
                OnGet();
                return Page();
            }
        }

    }
}
