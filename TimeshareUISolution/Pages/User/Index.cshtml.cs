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
    public class IndexModel : PageModel
    {
        private readonly IDepartmentService _service;
        public IndexModel(IDepartmentService service)
        {
            _service = service;
        }
        public List<DepartmentViewModel> DepartmentList { get; set; }
        public IActionResult OnGet()
        {
            var response = _service.GetModelAsync<DynamicModelsResponse<DepartmentViewModel>>(path: "/GetListDepartment").Result;
            if (response.Item1 != null)
            {
                if (response.Item1 != null)
                {
                    DepartmentList = response.Item1.Results;
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
