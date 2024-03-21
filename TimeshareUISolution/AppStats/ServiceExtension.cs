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
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IAvableTimeService, AvableTimeService>();
            services.AddScoped<IReservationService, ReservationService>();
        }
    }
}
