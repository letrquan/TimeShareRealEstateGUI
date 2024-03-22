namespace APIDataAccess.DTO.ResponseModels
{
    public class DepartmentOfProjectViewModel
    {
        public DepartmentOfProjectViewModel()
        {
            AvailableTimes = new HashSet<AvailableTimeViewModel>();
        }
        public int? DepartmentId { get; set; }
        public int? ProjectId { get; set; }
        public string? DepartmentProjectCode { get; set; }
        public ICollection<AvailableTimeViewModel>? AvailableTimes { get; set; }

    }
}
