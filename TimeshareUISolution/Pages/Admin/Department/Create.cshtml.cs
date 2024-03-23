using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.Admin.Department
{
    public class CreateModel : PageModel
    {
        private readonly IDepartmentService _service;
        private readonly IAccountService _accountService;
        public CreateModel(IDepartmentService service, IAccountService accountService)
        {
            _service = service;
            _accountService = accountService;
        }
        public Dictionary<int, string> ConstructionType { get; set; }
        public Dictionary<int, string> Status { get; set; }
        public List<AccountViewModel> ListAccount { get; set; }
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
            if (user.Value.Role != ((int)AccountRole.ADMIN))
            {
                RedirectToPage("/Admin/Login");
            }
            var contructionType = _service.GetModelAsync<EnumViewModel>
                (path: "/GetDepartmentConstructionType", token: user.AccessToken).Result;
            if (contructionType.Item1 != null)
            {
                if (contructionType.Item1.Results != null)
                {
                    ConstructionType = contructionType.Item1.Results;
                }
                else
                {
                    TempData["errorMessage"] = contructionType.Item1.Message;
                    return RedirectToPage("/Admin/Department/Index");
                }
            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return RedirectToPage("/Admin/Department/Index");
            }
            var status = _service.GetModelAsync<EnumViewModel>
                (path: "/GetDepartmentStatuses", token: user.AccessToken).Result;
            if (status.Item1 != null)
            {
                if (status.Item1.Results != null)
                {
                    Status = status.Item1.Results;
                }
                else
                {
                    TempData["errorMessage"] = status.Item1.Message;
                    return RedirectToPage("/Admin/Department/Index");
                }
            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return RedirectToPage("/Admin/Department/Index");
            }
            var account = _accountService.GetModelAsync<DynamicModelsResponse<AccountViewModel>>
                               (path: "/GetListAccount", token: user.AccessToken).Result;
            if (account.Item1.Results != null)
            {
                ListAccount = account.Item1.Results;
            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return RedirectToPage("/Admin/Department/Index");
            }
            return Page();
        }
        public IActionResult OnPost(string name, string address, string city, string country, string state, int floor, decimal price, string description, int capacity)
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
            if (user.Value.Role != ((int)AccountRole.ADMIN))
            {
                RedirectToPage("/Admin/Login");
            }
            var request = new DepartmentRequestModel
            {
                DepartmentName = name,
                Address = address,
                City = city,
                Country = country,
                State = state,
                Floors = floor,
                Price = price,
                Description = description,
                ConstructionType = int.Parse(Request.Form["constructionType"]),
                Status = int.Parse(Request.Form["status"]),
                OwnerId = int.Parse(Request.Form["ownerId"]),
                Capacity = capacity,
            };
            var response = _service.PostWithResponse<ResponseResult<DepartmentViewModel>, DepartmentRequestModel>(request ,path: "/CreateDepartment", user.AccessToken).Result;
            if(response.Item1 != null)
            {
                if(response.Item1.Value != null)
                {
                    TempData["successMessage"] = response.Item1.Message;
                    return RedirectToPage("/Admin/Department/Index");
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
