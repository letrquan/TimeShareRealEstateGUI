using APIDataAccess.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class UsageHistoryService : BaseService, IUsageHistoryService
    {
        public UsageHistoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = Utils.StoredURI.UsageHistory;
        }
    }
}
