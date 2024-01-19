using APIDataAccess.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements.Response
{
    public class TournamentService : BaseService, ITournamentService
    {
        public TournamentService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.Tournament;
        }
    }
}
