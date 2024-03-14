using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class AvailableTimeRequestModel
    {
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.DateTime)]
        [Compare("StartDate", ErrorMessage = "End date must be greater than start date")]
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Department Id is Invalid!")]
        public int? DepartmentId { get; set; }
    }
}
