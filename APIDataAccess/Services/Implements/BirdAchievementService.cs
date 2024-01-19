using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class BirdAchievementService : BaseService, IBirdAchievementService
    {
        public BirdAchievementService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.BirdAchievement;
        }
    }
}
