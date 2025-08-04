using MongoDB.Driver;
using Orange_Tree.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// DI
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connectionString = Environment.GetEnvironmentVariable("ORANGE_TREE_URI");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("ORANGE_TREE_URI not set");
    }
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<BlogService>();

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
