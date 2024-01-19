using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class ScoringCriteriaService : BaseService, IScoringCriteriaService
    {
        public ScoringCriteriaService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.ScoringCriteria;
        }
    }
}
