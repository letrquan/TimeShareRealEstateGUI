using APIDataAccess.DTO.ResponseModels;
using APIDataAccess.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static APIDataAccess.DTO.ResponseModels.Helpers.DynamicModelResponse;

namespace TimeshareUISolution.Pages.User
{
    public class PropertyModel : PageModel
    {
        private readonly IDepartmentService _service;
        public PropertyModel(IDepartmentService service)
        {
            _service = service;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 6;
        public List<DepartmentViewModel> PropertyList { get; set; }

        public IActionResult OnGet(int pageNumber)
        {
            if (pageNumber <= 0 || pageNumber == null)
            {
                pageNumber = 1;
            }
            var response = _service.GetModelAsync<DynamicModelsResponse<DepartmentViewModel>>(path: $"/GetListDepartment?page={pageNumber}&pageSize=6").Result;
            if (response.Item1 != null)
            {
                if (response.Item1.Results != null)
                {
                    PropertyList = response.Item1.Results;
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
            else
            {
                var errorMessage = response.Item2;
            }

            return Page();

        }
    }
}
