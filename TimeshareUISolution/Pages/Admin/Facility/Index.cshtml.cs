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
    public class IndexModel : PageModel
    {
        private readonly IFacilityService _service;
        public IndexModel(IFacilityService service)
        {
            _service = service;
        }
        public List<FacilityViewModel> FacilityList { get; set; }
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
            var response = _service.GetModelAsync<DynamicModelsResponse<FacilityViewModel>>(path: "/GetListFacility", token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1 != null)
                {
                    FacilityList = response.Item1.Results;
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
