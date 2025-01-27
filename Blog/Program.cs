using Blog.Mappers;
using Core;
using Ifrastructure;
using Infrastructure.Database.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
        sqlOptions.MigrationsAssembly("Infrastructure") 
    )
);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Index"; 
            options.AccessDeniedPath = "/AccessDenied"; 
        });

builder.Services.RegisterInfrastructure();

builder.Services.RegisterCoreServices();

builder.Services.AddAutoMapper(typeof(UserProfile)); 

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");


    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
