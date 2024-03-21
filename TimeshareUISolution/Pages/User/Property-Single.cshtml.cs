using APIDataAccess.DTO.RequestModels;
using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Services.Implements;
using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.User
{
    public class Property_SingleModel : PageModel
    {
        private readonly IDepartmentService _service;
        private readonly IAvableTimeService _serviceAvableTime;
        private readonly IReservationService _reservationService;
        public Property_SingleModel(IDepartmentService service, IAvableTimeService avableTimeService, IReservationService reservationService)
        {
            _service = service;
            _serviceAvableTime = avableTimeService;
            _reservationService =reservationService;
        }
        public DepartmentViewModel Property { get; set; }
        public List<AvailableTimeViewModel> AvailableTimeList { get; set; }
        public void OnGet(int id)
        {
            var response = _service.GetModelAsync<ResponseResult<DepartmentViewModel>>(path: $"/GetDepartment/{id}").Result;
            if (response.Item1 != null)
            {
                if (response.Item1 != null)
                {
                    Property = response.Item1.Value;
                }
                else
                {
                }
            }
            else
            {
                var errorMessage = response.Item2;
            }
            var availableTimeResponse = _serviceAvableTime.GetModelAsync<DynamicModelsResponse<AvailableTimeViewModel>>(path: $"/GetListAvailableTime?DepartmentId={id}").Result;
            if (availableTimeResponse.Item1 != null)
            {
                if (availableTimeResponse.Item1.Results != null)
                {
                    AvailableTimeList = availableTimeResponse.Item1.Results;
                }
                else
                {
                    var errorMessage = availableTimeResponse.Item1.Message;
                    if (errorMessage != null)
                    {
                    }
                }
            }
            else
            {
                var errorMessage = availableTimeResponse.Item2;
            }
        }
        public IActionResult OnPost(string reservationFee, int departmentId)
        {
            var userStr = HttpContext.Session.GetString("User");
            if (userStr == null || userStr.Count() == 0)
            {
                TempData["errorMessage"] = "Please login to continue";
                return RedirectToPage("/Admin/Login");
            }
            var user = JsonConvert.DeserializeObject<UserLoginResponse>(userStr);
            if (user == null)
            {
                TempData["errorMessage"] = "Please login to continue";
                return RedirectToPage("/Admin/Login");
            }
            if (user.Value.Role != ((int)AccountRole.CUSTOMER))
            {
                TempData["errorMessage"] = "Only Customer can book a reservation";
                OnGet(departmentId);
                return Page();
            }
            var avableTimeId = Request.Form["avableTimeId"];
            var parsedAvailableTimeId = int.TryParse(avableTimeId, out int availableTimeId) ? availableTimeId : 0; // Replace 0 with a default value or handle the error as needed

            var parsedReservationFee = decimal.TryParse(reservationFee, out decimal reservationFeeValue) ? reservationFeeValue : 0; // Replace 0 with a default value or handle the error as needed

            var avable = new ReservationRequestModel
            {
                CustomerId = (int)user.Value.AccountId,
                AvailableTimeId = parsedAvailableTimeId,
                ReservationFee = parsedReservationFee,
                Status = 3,
                ReservationDate = DateTime.Now
            };

            var respone = _reservationService.PostWithResponse<ResponseResult<ReservationViewModel>, ReservationRequestModel>(avable ,path: "/CreateReservation", token: user.AccessToken).Result;
            if (respone.Item1 != null)
            {
                if (respone.Item1.Value != null)
                {
                    TempData["successMessage"] = respone.Item1.Message;
                }
                else
                {
                    TempData["errorMessage"] = respone.Item1.Message;
                }
            }
            else
            {
                TempData["errorMessage"] = respone.Item2.Title;
            }
            OnGet(departmentId);
            return Page();
        }
    }
}
