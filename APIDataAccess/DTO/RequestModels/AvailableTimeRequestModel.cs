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
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public string? DepartmentProjectCode { get; set; }
    }
}
