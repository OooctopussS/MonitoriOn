using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(Options => {
    Options.IdleTimeout = TimeSpan.FromMinutes(20);
    Options.Cookie.HttpOnly = true;
    Options.Cookie.IsEssential = true;
});

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

//builder.Services.AddScoped<SignInManager<MonitoriOnUser>>();
//builder.Services.AddScoped<UserManager<MonitoriOnUser>>();
//builder.Services.AddScoped<UserStore<MonitoriOnUser>>();
//builder.Services.AddScoped<RoleManager<MonitoriOnUser>>();

//builder.Services.AddIdentity<MonitoriOnUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
builder.Services.AddIdentity<MonitoriOnUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
    //.AddSignInManager<SignInManager<MonitoriOnUser>>()
    //.AddUserManager<UserManager<MonitoriOnUser>>()
    //.AddUserStore<UserStore<MonitoriOnUser>>()
    //.AddRoleManager<RoleManager<IdentityRole>>();
    

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
