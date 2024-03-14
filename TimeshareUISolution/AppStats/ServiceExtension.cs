using APIDataAccess.Services.Implements;
using APIDataAccess.Services.IService;

namespace TimeshareUISolution.AppStats
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
        }
    }
}
