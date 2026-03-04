using catalog.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// CHANGE THIS to false WHEN MIGRATING TO RDS
var useInMemory = true;

if (useInMemory)
{
    builder.Services.AddDbContext<BookContext>(options =>
        options.UseInMemoryDatabase("test"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("BooksDB");
    builder.Services.AddDbContext<BookContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

// Production vs Dev URL configuration
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    Console.WriteLine("Running in production mode...");
    builder.WebHost.UseUrls("http://*:8080");
}
else
{
    Console.WriteLine("Running in dev mode...");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();