using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.User
{
    public class ReservationModel : PageModel
    {
        private readonly IReservationService _reservationService;
        private readonly IAvableTimeService _availableTimeService;
        public ReservationModel(IReservationService reservationService, IAvableTimeService avableTimeService)
        {
            _reservationService = reservationService;
            _availableTimeService = avableTimeService;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 6;
        public List<ReservationViewModel> Reservations { get; set; }
        public IActionResult OnGet(int pageNumber)
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
            if (user.Value.Role != ((int)AccountRole.CUSTOMER))
            {
                TempData["ErrorMessage"] = "Only Customer can access this page";
                return RedirectToPage("/User/Index");
            }
            if (pageNumber <= 0 || pageNumber == null)
            {
                pageNumber = 1;
            }
            var response = _reservationService.GetModelAsync<DynamicModelsResponse<ReservationViewModel>>(path: $"/GetListReservation?CustomerId={user.Value.AccountId}&page={pageNumber}&pageSize=6", token:user.AccessToken).Result;
            if (response.Item1 != null)
            {
                if (response.Item1 != null)
                {
                    Reservations = response.Item1.Results;
                }
                else
                {
                    TempData["ErrorMessage"] = response.Item1.Message;
                    return RedirectToPage("/");
                }

            }
            return Page();
        }
    }
}
