using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class CustomerRequestRequestModel
    {
        public int? CustomerRequestId { get; set; }
        public int? CustomerId { get; set; }
        public int? RequestType { get; set; }
        public string? Description { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Status { get; set; }
        public int? DepartmentId { get; set; }
    }
}
