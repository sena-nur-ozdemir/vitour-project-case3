using Case3Vitour.Services.CategoryServices;
using Case3Vitour.Services.GalleryServices;
using Case3Vitour.Services.ReportServices;
using Case3Vitour.Services.ReservationServices;
using Case3Vitour.Services.ReviewServices;
using Case3Vitour.Services.TourPlanServices;
using Case3Vitour.Services.TourServices;
using Case3Vitour.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor; 

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

// --- 1. Çoklu Dil (Localization) Servis Kaydý ---
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources"; // Dil dosyalarýnýn (resx) bakýlacaðý klasör
});

// --- 2. Veritabaný Ayarlarý ---
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettingKey"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

// --- 3. Servis Kayýtlarý (Dependency Injection) ---
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITourPlanService, TourPlanService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationValidator, ReservationValidator>();

// --- 4. MVC ve View Localization Yapýlandýrmasý ---
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) // View'larda dil desteði
    .AddDataAnnotationsLocalization(); // Model validation'larda dil desteði

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// --- 5. Dil Middleware Yapýlandýrmasý (Kritik Bölüm) ---
var supportedCultures = new[] { "tr", "en" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0]) // Varsayýlan dil Türkçe
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// --- 6. Middleware Yapýlandýrmasý ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();