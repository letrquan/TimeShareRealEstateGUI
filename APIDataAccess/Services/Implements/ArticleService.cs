using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class ArticleService : BaseService, IArticleService
    {
        public ArticleService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Article;
        }
    }
}
