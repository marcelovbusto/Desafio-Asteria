using AsteriaChallenger.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// 1) Aumentar limite para multipart/form-data (upload via <form>):
builder.Services.Configure<FormOptions>(options =>
{
  // Exemplo: 200 MB
  options.MultipartBodyLengthLimit = 200L * 1024L * 1024L;
});

// 2) Configurar Kestrel para aceitar um Request Body maior:
builder.WebHost.ConfigureKestrel(options =>
{
  // 200 MB
  options.Limits.MaxRequestBodySize = 200L * 1024L * 1024L;
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  dbContext.Database.Migrate(); // Executa migrations ao iniciar
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
