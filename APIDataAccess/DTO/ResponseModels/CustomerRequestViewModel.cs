namespace APIDataAccess.DTO.ResponseModels
{
    public class CustomerRequestViewModel
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