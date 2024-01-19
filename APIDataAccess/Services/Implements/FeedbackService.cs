using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class FeedbackService : BaseService, IFeedbackService
    {
        public FeedbackService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Feedback;
        }
    }
}
