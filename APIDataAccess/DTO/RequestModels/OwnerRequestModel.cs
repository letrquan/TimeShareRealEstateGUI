namespace APIDataAccess.DTO.RequestModels
{
    public class OwnerRequestModel
    {
        public OwnerRequestModel()
        {
            Departments = new HashSet<DepartmentRequestModel>();
        }

        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? Status { get; set; }

        public ICollection<DepartmentRequestModel>? Departments { get; set; }
    }
}
