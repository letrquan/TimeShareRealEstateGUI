using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace TimeshareUISolution.Pages.Admin.Department
{
    public class EditModel : PageModel
    {
        private readonly IDepartmentService _service;
        private readonly IOwnerService _ownerService;
        public EditModel(IDepartmentService service, IOwnerService ownerService)
        {
            _service = service;
            _ownerService = ownerService;
        }
        public DepartmentViewModel Department { get; set; }
        public OwnerViewModel Owner { get; set; }   
        public Dictionary<int, string> Enum { get; set; }
        public Dictionary<int, string> Status { get; set; }
        public IActionResult OnGet(string departmentId)
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
            var response = _service.GetModelAsync<ResponseResult<DepartmentViewModel>>(path: $"/GetDepartment/{departmentId}", token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1.Value != null)
                {
                    Department = response.Item1.Value;
                    var enumResponse = _service.GetModelAsync<EnumViewModel>(path: $"/GetDepartmentConstructionType", token: user.AccessToken).Result;
                    if (enumResponse.Item1 != null)
                    {
                        if (enumResponse.Item1.Results != null)
                        {
                            Enum = enumResponse.Item1.Results;
                        }
                        else
                        {
                            TempData["errorMessage"] = enumResponse.Item1.Message;
                            return RedirectToPage("/Admin/Department/Index");
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Server error";
                        return RedirectToPage("/Admin/Department/Index");
                    }
                    var statusResponse = _service.GetModelAsync<EnumViewModel>(path: $"/GetDepartmentStatuses", token: user.AccessToken).Result;
                    Status = statusResponse.Item1.Results;
                    var ownerResponse = _ownerService.GetModelAsync<ResponseResult<OwnerViewModel>>(path: $"/GetOwner/{Department.OwnerId.ToString()}", token: user.AccessToken).Result;
                    if (ownerResponse.Item1 != null)
                    {
                        if (ownerResponse.Item1.Value != null)
                        {
                            Owner = ownerResponse.Item1.Value;
                        }
                        else
                        {
                            TempData["errorMessage"] = ownerResponse.Item1.Message;
                            return RedirectToPage("/Admin/Department/Index");
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Server error";
                        return RedirectToPage("/Admin/Department/Index");
                    }
                    return Page();
                }
                else
                {
                    TempData["errorMessage"] = response.Item1.Message;
                    return RedirectToPage("/Admin/Department/Index");
                }

            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return Page();
            }
        }
        public IActionResult OnPost(string ownerId, string departmentName, string address, string city, string state, string country, int capacity, int floor, decimal price, string description, int departmentId)
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
            var update = new DepartmentRequestModel
            {
                OwnerId = int.Parse(ownerId),
                DepartmentName = departmentName,
                Address = address,
                City = city,
                State = state,
                Country = country,
                Capacity = capacity,
                Floors = floor,
                Price = price,
                Description = description,
                ConstructionType = int.Parse(Request.Form["constructionType"]),
                Status = int.Parse(Request.Form["status"])
            };
            var response = _service.Put<DepartmentRequestModel, DepartmentViewModel>(update, path: $"/UpdateDepartment/{departmentId}", token: user.AccessToken).Result;
            if (response != null)
            {
                if (response.result == true)
                {
                    TempData["successMessage"] = response.Message;
                    return Redirect($"/Admin/Department/Edit?departmentId={departmentId}");
                }
                else
                {
                    OnGet(departmentId.ToString());
                    TempData["errorMessage"] = response.Message;
                    return Page();
                }
            }
            else
            {
                OnGet(departmentId.ToString());
                TempData["errorMessage"] = "Server error";
                return Page();
            }
        }

    }
}
