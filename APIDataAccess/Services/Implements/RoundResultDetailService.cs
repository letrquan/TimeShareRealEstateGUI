using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class RoundResultDetailService : BaseService, IRoundResultDetailService
    {
        public RoundResultDetailService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.RoundResultDetail;
        }
    }
}
