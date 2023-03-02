
using CI_Platform.Model;
using CI_Platfrom.Entities.Data;
using CI_Platfrom.Repository.Interface;
using CI_Platfrom.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CiPlatformContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Data Source=PCA39\\SQL2017;DataBase=CI_PLATFORM;User ID=sa;Password=Tatva@123;Encrypt=False")
    ));


/// <summary>
///  session will be expire automatically after 60 minutes 
/// </summary>
/// 

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<IMissionRepository, MissionRepository>();


//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=login}");

app.MapControllerRoute(
    name: "Authentication",
    pattern: "{controller=Authentication}/{action=login}");

app.MapControllerRoute(
    name: "Authentication",
    pattern: "{controller=Authentication}/{action=forgotPassword}");

app.MapControllerRoute(
    name: "Registration",
    pattern: "{controller=Registration}/{action=Register}");

app.MapControllerRoute(
    name: "Registration",
    pattern: "{controller=Mission}/{action=LandingPage}");

app.MapControllerRoute(
    name: "Registration",
    pattern: "{controller=Story}/{action=StoryListingPage}");


app.Run();
