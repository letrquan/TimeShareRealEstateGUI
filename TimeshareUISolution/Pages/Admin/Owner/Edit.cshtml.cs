using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.Admin.Owner
{
    public class EditModel : PageModel
    {
        private readonly IOwnerService _service;
        public EditModel(IOwnerService service)
        {
            _service = service;
        }
        public OwnerViewModel Owner { get; set; }

        public Dictionary<int, string> Enum { get; set; }
        public Dictionary<int, string> Status { get; set; }
        public IActionResult OnGet(string ownerId)
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
            var response = _service.GetModelAsync<ResponseResult<OwnerViewModel>>(path: $"/GetOwner/{ownerId}", token: user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1.Value != null)
                {
                    Owner = response.Item1.Value;
                    var statusResponse = _service.GetModelAsync<EnumViewModel>(path: $"/GetOwnerStatus", token: user.AccessToken).Result;
                    Status = statusResponse.Item1.Results;
                    
                    
                    
                    return Page();
                }
                else
                {
                    TempData["errorMessage"] = response.Item1.Message;
                    return RedirectToPage("/Admin/Owner/Index");
                }

            }
            else
            {
                TempData["errorMessage"] = "Server error";
                return Page();
            }
        }
        public IActionResult OnPost(int ownerId, string ownerName, string contactPerson, string email, string phone)
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
            var update = new OwnerRequestModel
            {
                OwnerId = ownerId,
                OwnerName = ownerName,
                ContactPerson = contactPerson,
                Email = email,
                Phone = phone,
                Status = int.Parse(Request.Form["status"]),

            };
            var response = _service.Put<OwnerRequestModel, OwnerViewModel>(update, path: $"/UpdateOwner/{ownerId}", token: user.AccessToken).Result;
            if (response != null)
            {
                if (response.result == true)
                {
                    TempData["successMessage"] = response.Message;
                    return Redirect($"/Admin/Owner/Edit?OwnerId={ownerId}");
                }
                else
                {
                    OnGet(ownerId.ToString());
                    TempData["errorMessage"] = response.Message;
                    return Page();
                }
            }
            else
            {
                OnGet(ownerId.ToString());
                TempData["errorMessage"] = "Server error";
                return Page();
            }
        }

    }
}
