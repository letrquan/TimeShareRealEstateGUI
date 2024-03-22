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
    public class IndexModel : PageModel
    {
        private readonly IDepartmentService _service;
        public static int CurrentPage { get; set; }
        public static int TotalPage { get; set; }
        public int PageSize { get; set; } = 3;
        public IndexModel(IDepartmentService service)
        {
            _service = service;
        }
        public int PageNumber { get; set; }
        public List<DepartmentViewModel> DepartmentList { get; set; }
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
            var response = _service.GetModelAsync<DynamicModelsResponse<DepartmentViewModel>>
                (path: "/GetListDepartment?DepartmentName=" + filter + "&" + "page="  + CurrentPage + "&pageSize=" +PageSize, token: user.AccessToken).Result;

            TotalPage = (int)MathF.Ceiling((float)response.Item1.Metadata.Total / (float)response.Item1.Metadata.Size);

            if (response.Item1 != null)
            {
                if(response.Item1 != null)
                {
                    DepartmentList = response.Item1.Results;
                    PageNumber = response.Item1.Metadata.Page;
                }
                else
                {
                    var errorMessage = response.Item1.Message;
                    if (errorMessage != null)
                    {
                    }
                }
                
            }
            return Page();
        }

        public IActionResult OnPostSearch()
        {
            string search = Request.Form["search"];
            return OnGet(filter: search);
        }
    }
}
