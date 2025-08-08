using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using TestAAI.Data;
using TestAAI.Interfaces;
using TestAAI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Nombre del Key Vault (solo el nombre, sin https://)
string keyVaultName = builder.Configuration["KeyVaultName"];
var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");

// Cliente de Key Vault con Managed Identity
var secretClient = new SecretClient(vaultUri: keyVaultUri, credential: new DefaultAzureCredential());

// Obtener el secreto (cadena de conexión)
KeyVaultSecret secret = secretClient.GetSecret("SqlConnectionString");
string connectionString = secret.Value;

// Add services to the container.
// Servicios MVC + EF Core + Inyección de dependencias
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

// Ruta por defecto sigue siendo Home, pero puedes acceder a /User
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
