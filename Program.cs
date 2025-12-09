using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<SqlConnectionFactory>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ServicioRepository>();
builder.Services.AddScoped<MascotaRepository>();
builder.Services.AddScoped<ChatBotRepository>();
builder.Services.AddScoped<CitaRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
