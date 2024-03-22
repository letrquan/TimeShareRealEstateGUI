using APIDataAccess.Services.IService;

namespace APIDataAccess.Services.Implements
{
    public class ReservationService:BaseService,IReservationService
    {
        public ReservationService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = Utils.StoredURI.Reservation;
        }
    }

}
