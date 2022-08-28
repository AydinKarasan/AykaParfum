using AppCoreV2.DataAccess.Configs;
using AykaParfum.Settings;
using Business.Services;
using Business.Services.Hesap;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

List<CultureInfo> _cultures = new List<CultureInfo>()
{
    new CultureInfo("tr-TR")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(_cultures.FirstOrDefault().Name);
    options.SupportedCultures = _cultures;
    options.SupportedUICultures = _cultures;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// yetkilendirmeyi özelleþtiriyoruz// cookie iþlemleri
 builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
{
    config.LoginPath = "/Hesaplar/Giris";
    config.AccessDeniedPath = "/Hesaplar/YetkisizIslem";
    config.ExpireTimeSpan = TimeSpan.FromMinutes(40);
    config.SlidingExpiration = true;
});

builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(40); //default: 20 minutes
});

ConnectionConfig.ConnectionString = builder.Configuration.GetConnectionString("AykaParfumContext");

//imaj kaydetmek için appsetting class ý oluþturulunca kullanýlacak
IConfiguration section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());

#region IoC Container (Inversion of Control) /*dependansy injection kýsmý*/
builder.Services.AddScoped<IKategoriService, KategoriService>();
builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IMarkaService, MarkaService>();
builder.Services.AddScoped<IHesapService, HesapService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<IUlkeService, UlkeService>();
builder.Services.AddScoped<ISehirService, SehirService>();
builder.Services.AddScoped<IUrunRaporService, UrunRaporService>();
builder.Services.AddScoped<IKampanyaService, KampanyaService>();

#endregion

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(_cultures.FirstOrDefault().Name),
    SupportedCultures = _cultures,
    SupportedUICultures = _cultures,
});

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}" /*buraya areadaki conroller route yazýlacak*/
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();