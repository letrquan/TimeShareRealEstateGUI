using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class RoundResultService : BaseService, IRoundResultService
    {
        public RoundResultService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.RoundResult;
        }
    }
}
