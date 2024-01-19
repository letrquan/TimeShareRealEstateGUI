using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services.Implements
{
    public class RegisterFormService : BaseService, IRegisterFormService
    {
        public RegisterFormService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            Path = StoredURI.RegisterForm;
        }
    }
}
