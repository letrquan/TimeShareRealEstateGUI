using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class FeedbackRequestModel
    {
        public int? FeedbackId { get; set; }
        public int? CustomerId { get; set; }
        public double? Rating { get; set; }
        public string? Content { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}
