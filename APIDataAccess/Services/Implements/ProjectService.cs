using APIDataAccess.Services.IService;
using APIDataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Project;
        }
    }
    
}
