using Homework.Api.Interfaces;
using Homework.Api.ProductProviders;
using Homework.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register DummyJsonProductProvider and inject a httpClient
builder.Services.AddHttpClient<DummyJsonProductProvider>();
//Register DummyJsonProductProvider as an implementation of IProductProvider
builder.Services.AddScoped<IProductProvider, DummyJsonProductProvider>();
//Register ProductService and inject DummyJsonProductProvider as an implementation of IProductProvider
builder.Services.AddScoped<ProductService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    options.AddPolicy("ProductionCorsPolicy", policy =>
    {
        policy.WithOrigins("https://production.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevCorsPolicy");
}
else if (app.Environment.IsProduction())
{
    app.UseCors("ProductionCorsPolicy");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
