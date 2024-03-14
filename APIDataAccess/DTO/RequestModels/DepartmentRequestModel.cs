using APIDataAccess.DTO.ResponseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class DepartmentRequestModel
    {

        [Required(ErrorMessage = "Please enter department name!")]
        public string? DepartmentName { get; set; }
        public int? ProjectId { get; set; }
        public int? OwnerId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int? Floors { get; set; }
        public decimal? Price { get; set; }
        public int? ConstructionType { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public int? Capacity { get; set; }

        public ICollection<AvailableTimeViewModel>? AvailableTimes { get; set; }
        public ICollection<CustomerRequestViewModel>? CustomerRequests { get; set; }
        public ICollection<FacilityViewModel>? Facilities { get; set; }
        public ICollection<FeedbackViewModel>? Feedbacks { get; set; }
        public ICollection<UsageHistoryViewModel>? UsageHistories { get; set; }
    }
}
