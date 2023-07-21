using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using QFamilyForum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QFamilyForum.Services;
using QFamilyForum.Interface;

var builder = WebApplication.CreateBuilder(args);

List<string>? possibleWordlesOrNull = builder.Configuration.GetSection("PossibleWordles").Get<List<string>>();
if(possibleWordlesOrNull == null)
{
    throw new InvalidOperationException("No possible wordles found in configuration.");
}
List<string> possibleWordles = possibleWordlesOrNull;

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<QFamilyForumContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QFamilyForumContext") ?? throw new InvalidOperationException("Connection string 'QFamilyForumContext' not found.")));
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped(typeof(List<string>), provider => possibleWordlesOrNull);
builder.Services.AddScoped<IGameEngine, GameEngine>();
builder.Services.AddScoped<IWordleGenerator, WordleGenerator>();
builder.Services.AddScoped<MineService>();
builder.Services.AddScoped<ITalker, TalkerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
// its main function is, decouple the URL and the controller
// app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); // used in the MVC
app.MapControllers(); // used in API


// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QFamilyForumContext>();
    if(db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}
app.Run();
