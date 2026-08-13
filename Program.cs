using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCLanche.Context;
using MVCLanche.Models;
using MVCLanche.Repositories;
using MVCLanche.Repositories.Interfaces;
using MVCLanche.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Configuração da sessão do usuário
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo limite da sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' não encontrada.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        // Configurações de Password
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;

        // Lockout
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ILancheRepository, LancheRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<IUploadImagemService, UploadImagemService>();
builder.Services.AddScoped<GraficoVendasService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

var supportedCultures = new[] { new CultureInfo("pt-BR") };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<ISeedUserRoleInitial>();

    seed.SeedRoles();
    seed.SeedUsers();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { controller = "Lanche", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=List}/{id?}");

app.Run();
