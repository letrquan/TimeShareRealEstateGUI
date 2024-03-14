namespace APIDataAccess.DTO.ResponseModels
{
    public class OwnerViewModel
    {
        public OwnerViewModel()
        {
            Departments = new HashSet<DepartmentViewModel>();
        }

        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? Status { get; set; }

        public ICollection<DepartmentViewModel>? Departments { get; set; }
    }
}
