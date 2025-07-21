using Btc.Api.Contexts;
using Btc.Api.Mappers;
using Btc.Api.Repositories;
using Btc.Api.Repositories.Interfaces;
using Btc.Api.Services;
using Btc.Api.Services.BackgroundServices;
using Btc.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/btc-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add DB
builder.Services.AddDbContext<CurrencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CurrencyDb")));

// repos
builder.Services.AddScoped<IBitcoinRateRecordRepository, BitcoinRateRecordRepository>();
builder.Services.AddScoped<IBitcoinRateRecordSnapshotRepository, BitcoinRateRecordSnapshotRepository>();
builder.Services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// mappers
builder.Services.AddAutoMapper(typeof(BitcoinRateRecordProfile));
builder.Services.AddAutoMapper(typeof(BitcoinRateRecordSnapshotProfile));
builder.Services.AddAutoMapper(typeof(CurrencyRateProfile));

// background services
builder.Services.AddHostedService<CoinDeskPollingService>();
builder.Services.AddHostedService<CnbPollingService>();

// services
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IBitcoinService, BitcoinService>();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IHttpService, HttpService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
