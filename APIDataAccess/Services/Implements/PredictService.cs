using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class PredictService : BaseService, IPredictService
    {
        public PredictService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Predict;
        }
    }
}
