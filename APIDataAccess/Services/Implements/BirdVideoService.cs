using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class BirdVideoService : BaseService, IBirdVideoService
    {
        public BirdVideoService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.BirdVideos;
        }
    }
}
