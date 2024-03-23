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
        //[RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Project Id is Invalid!")]
        //public int? ProjectId { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Owner Id is Invalid!")]
        public int? OwnerId { get; set; }
        public string? Address { get; set; }
        //public string? DepartmentProjectCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Floors is Invalid!")]
        public int? Floors { get; set; }
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Price is Invalid!")]
        public decimal? Price { get; set; }
        public int? ConstructionType { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Capacity is Invalid!")]
        public int? Capacity { get; set; }
        public ICollection<DepartmentOfProjectDepartmentRequestModel>? DepartmentOfProjects { get; set; }

        public ICollection<AvailableTimeViewModel>? AvailableTimes { get; set; }
        public ICollection<CustomerRequestViewModel>? CustomerRequests { get; set; }
        public ICollection<FacilityViewModel>? Facilities { get; set; }
        public ICollection<FeedbackViewModel>? Feedbacks { get; set; }
        public ICollection<UsageHistoryViewModel>? UsageHistories { get; set; }
    }
}
