
using CI_Platfrom.Entities.Data;
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
