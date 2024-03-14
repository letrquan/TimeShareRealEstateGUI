namespace APIDataAccess.DTO.ResponseModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {
            UsageRights = new HashSet<UsageRightViewModel>();
        }

        public int? ReservationId { get; set; }
        public int? CustomerId { get; set; }
        public int? AvailableTimeId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public decimal? ReservationFee { get; set; }
        public int? Status { get; set; }

        public ICollection<UsageRightViewModel>? UsageRights { get; set; }
    }
}