using APIDataAccess.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TimeshareUISolution.Pages.Admin.AvaibleTime
{
    public class IndexModel : PageModel
    {
        private readonly IAvableTimeService _avableTimeService;
        public IndexModel(IAvableTimeService avableTimeService)
        {
            _avableTimeService = avableTimeService;
        }
        public void OnGet()
        {
        }
    }
}
