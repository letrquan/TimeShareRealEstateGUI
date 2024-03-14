namespace APIDataAccess.DTO.RequestModels.Helpers
{
    public class PagingRequest
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
