using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Net.Http.Headers;
using TimeshareUISolution.AppStats;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
	options.Conventions.AddPageRoute("/User/Index", "/");
});
builder.Services
    .AddHttpClient("BaseClient", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetSection("API_URL").Value);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(googleOptions =>
    {
        // Đọc thông tin Google từ appsettings.json
        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Google");

        // Thiết lập ClientID và ClientSecret để truy cập API google
        googleOptions.ClientId = googleAuthNSection["ClientId"];
        googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
        googleOptions.Scope.Add("openid");
        googleOptions.Scope.Add("profile");
        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
        //googleOptions.CallbackPath = "/signin-google";

    });

builder.Services.AddHttpContextAccessor();

#region AppStarts
builder.Services.AddAutoMapper(typeof(AutoMapperResolver).Assembly);
builder.Services.AddServices();
#endregion

#region Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
