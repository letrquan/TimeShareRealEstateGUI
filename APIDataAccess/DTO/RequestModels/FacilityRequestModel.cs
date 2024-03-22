using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class FacilityRequestModel
    {
        public int? FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int? FacilityType { get; set; }
        public int? DepartmentId { get; set; }
    }
}
