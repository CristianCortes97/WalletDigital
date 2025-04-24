using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WalletApi.Application.UnitOfWork;
using WalletApi.Domain.Interface;
using WalletApi.Domain.Service;
using WalletApi.Infraestructure.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<WalletdigitaldbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WalletDigitalConnection")));
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
