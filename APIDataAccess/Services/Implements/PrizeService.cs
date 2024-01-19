using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class PrizeService : BaseService, IPrizeService
    {
        public PrizeService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Prize;
        }
    }
}
