namespace APIDataAccess.DTO.ResponseModels
{
    public class AccountViewModel
    {
        public AccountViewModel()
        {
            ContractCustomers = new HashSet<ContractViewModel>();
            ContractStaffs = new HashSet<ContractViewModel>();
            CustomerRequests = new HashSet<CustomerRequestViewModel>();
            Feedbacks = new HashSet<FeedbackViewModel>();
            Reservations = new HashSet<ReservationViewModel>();
            UsageHistories = new HashSet<UsageHistoryViewModel>();
            UsageRights = new HashSet<UsageRightViewModel>();
            StaffOfProjects = new HashSet<StaffOfProjectsViewModel>();
        }

        public int? AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? Status { get; set; }
        //[Ignore]
        public ICollection<ContractViewModel>? ContractCustomers { get; set; }
        //[Ignore]
        public ICollection<ContractViewModel>? ContractStaffs { get; set; }
        //[Ignore]
        public ICollection<CustomerRequestViewModel>? CustomerRequests { get; set; }
        //[Ignore]
        public ICollection<FeedbackViewModel>? Feedbacks { get; set; }
        //[Ignore]
        public ICollection<ReservationViewModel>? Reservations { get; set; }
        //[Ignore]
        public ICollection<UsageHistoryViewModel>? UsageHistories { get; set; }
        public ICollection<StaffOfProjectsViewModel>? StaffOfProjects { get; set; }
        //[Ignore]
        public ICollection<UsageRightViewModel>? UsageRights { get; set; }
        //[Ignore]
    }
}
