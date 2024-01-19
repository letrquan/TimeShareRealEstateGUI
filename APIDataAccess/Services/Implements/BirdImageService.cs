namespace APIDataAccess.Services.Implements
{
	public class BirdImageService : BaseService, IBirdImageService
	{
		public BirdImageService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			Path = StoredURI.BirdImages;
		}
	}
}