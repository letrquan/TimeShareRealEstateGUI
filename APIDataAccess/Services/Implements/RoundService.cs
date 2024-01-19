using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class RoundService : BaseService, IRoundService
    {
        public RoundService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Round;
        }
    }
}
