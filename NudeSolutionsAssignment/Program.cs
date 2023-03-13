using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NudeSolutionsAssignment.Data;
using NudeSolutionsAssignment.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.Configure<MongDbSettings>(builder.Configuration.GetSection(nameof(MongDbSettings)));
builder.Services.AddSingleton<IMongoDbSettings>(mongoDb=> mongoDb.GetRequiredService<IOptions<MongDbSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));
builder.Services.AddScoped<IItemCollectionService,ItemCollectionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapControllers();

app.MapFallbackToFile("index.html"); ;

app.Run();
