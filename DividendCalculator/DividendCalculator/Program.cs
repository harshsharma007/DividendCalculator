using DividendCalculator;
using DividendCalculator.Repositories;
using DividendCalculator.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DividendCalculator");
        services.AddDbContext<DividendCalculatorDbContext>(x => x.UseSqlServer(connectionString));

        services.AddScoped<IReadDataFromXLSFile, ReadDataFromXLSFile>();
        services.AddScoped<IInitiateAppService, InitiateAppService>();
        services.AddScoped<IPopulateDatabase, PopulateDatabase>();
    })
    .Build();

await builder.Services.GetRequiredService<IInitiateAppService>().ExecuteAsync();