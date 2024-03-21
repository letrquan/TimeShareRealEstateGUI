using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class FacilityService : BaseService, IFacilityService
    {
        public FacilityService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Facility;
        }
    }
}
