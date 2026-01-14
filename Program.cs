using ApiBanPlaz.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BanPlazDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("CadCnMySql"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("CadCnMySql")
        )
    );
});

builder.Services.AddScoped<CredApiService>();
builder.Services.AddScoped<CredApiRsService>();
builder.Services.AddScoped<NonceService>();
builder.Services.AddScoped<TokenDIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
