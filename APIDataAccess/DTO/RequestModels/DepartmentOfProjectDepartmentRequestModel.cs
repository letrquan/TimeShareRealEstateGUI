using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class DepartmentOfProjectDepartmentRequestModel
    {
        public int? ProjectId { get; set; }
        public string? DepartmentProjectCode { get; set; }
    }

}
