using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NDISBudget.Model;
using Serilog;
using NDIS.Data;

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed((hosts) => true));//AllowCredentials().
});
// Add services to the container.
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddInfrastructure();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
//builder.Services.addin;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // c.IncludeXmlComments(string.Format(@"{0}\NDISBudget.xml", System.AppDomain.CurrentDomain.BaseDirectory));
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Dapper - WebApi",
    });
});
builder.Services.AddDbContext<NdisbudgetContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/Forbidden/";
});
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Company",
        policy => policy.RequireClaim("Company"));
});

//builder.use

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseMvc(routes =>
//{

//    routes.MapAreaRoute(
//        name: "areaRoute",
//        areaName: "Admin",
//        template: "Admin/{controller=Account}/{action=Login}/{id?}"
//    );
//    routes.MapAreaRoute(
//        name: "areaRoute",
//        areaName: "Company",
//        template: "Company/{controller=Account}/{action=Login}/{id?}"
//    );
//});
app.MapControllerRoute(
 name: "default",
 pattern: "{area=Admin}/{controller=Main}/{action=landingpage}");
app.MapControllerRoute(
    name: "AdminLogin",
    pattern: "{area=Admin}/{controller=Account}/{action=Login}/{id?}");
app.MapControllerRoute(
name: "Company",
pattern: "{area=Company}/{controller=Account}/{action=Login}/{id?}");
//app.UseMvc();

//app.UseEndpoints()


app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(cookiePolicyOptions);
app.MapControllers();

app.Run();
