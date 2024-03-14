using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class ProjectRequestModel
    {
        [Required(ErrorMessage = "Please enter project name!")]
        public string ProjectName { get; set; }
        public int? PriorityType { get; set; }

        [Required(ErrorMessage = "Please enter project code!")]
        public string ProjectCode { get; set; }

        public int? TotalSlot { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Compare("StartDate", ErrorMessage = "End date must be greater than start date")]
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RegistrationEndDate { get; set; }

        [DataType(DataType.DateTime)]
        [Compare("RegistrationEndDate", ErrorMessage = "Registration opening date must greater than Registration end date!")]
        public DateTime? RegistrationOpeningDate { get; set; }
    }
}
