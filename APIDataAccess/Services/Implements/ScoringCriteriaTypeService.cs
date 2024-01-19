using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class ScoringCriteriaTypeService : BaseService, IScoringCriteriaTypeService
    {
        public ScoringCriteriaTypeService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.ScoringCriteriaType;
        }
    }
}
