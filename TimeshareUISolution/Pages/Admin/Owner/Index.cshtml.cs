using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.Admin.Owner
{
    public class IndexModel : PageModel
    {
        private readonly IOwnerService _service;
        public static int CurrentPage { get; set; }
        public static int TotalPage { get; set; }
        public IndexModel(IOwnerService service)
        {
            _service = service;
        }
        public List<OwnerViewModel> OwnerList { get; set; }
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
            var response = _service.GetModelAsync<DynamicModelsResponse<OwnerViewModel>>
                (path: "/GetListOwner?OwnerName=" + filter + "&page=" + CurrentPage, token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1 != null)
                {
                    OwnerList = response.Item1.Results;
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
    }
}