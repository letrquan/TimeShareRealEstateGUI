namespace APIDataAccess.Services.Implements
{
	public class BirdService:BaseService, IBirdService
	{
		public BirdService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			Path = StoredURI.Bird;
		}
	}

}