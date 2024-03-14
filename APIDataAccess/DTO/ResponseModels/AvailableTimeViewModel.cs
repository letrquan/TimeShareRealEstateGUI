namespace APIDataAccess.DTO.ResponseModels
{
    public class AvailableTimeViewModel
    {
        public AvailableTimeViewModel()
        {
            Contracts = new HashSet<ContractViewModel>();
            Reservations = new HashSet<ReservationViewModel>();
        }

        public int? AvailableTimeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? DepartmentId { get; set; }

        public ICollection<ContractViewModel>? Contracts { get; set; }
        public ICollection<ReservationViewModel>? Reservations { get; set; }
    }
}