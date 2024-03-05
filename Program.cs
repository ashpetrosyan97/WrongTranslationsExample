using EFCore.Examples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WrongTranslationsExample;

var builder = new HostBuilder().ConfigureServices(services =>
{
    services.AddDbContextPool<ExampleDbContext>(o => o
        .UseNpgsql(
            "host=store;Port=5432;Database=ExampleDb;Username=postgres;Password=kjsa*asf&ad(234);TrustServerCertificate=true")
        .UseSnakeCaseNamingConvention()
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging());
    services.AddLogging(p => p.AddConsole());
});
var app = builder.Build();
var context = app.Services.GetRequiredService<ExampleDbContext>();
var queryable = context.Users.Where(u =>
    u.Orders.GroupBy(o => o.StartDate.Date, (k, g) => g.Select(o => new OrderData
            {
                Id = o.Id,
                StartDate = o.StartDate,
                EndDate = o.EndDate
            })
            .Union(u.WorkingScheme.WorkingDays.Where(w => w.Day == k.DayOfWeek).SelectMany(
                a => context.Split("0,1", ","), (day, b) => new OrderData
                {
                    Id = b.Value == "0" ? 0 : int.MaxValue,
                    EndDate = b.Value == "0" ? k.Date + day.StartTime.ToTimeSpan() : k.Date + day.EndTime.ToTimeSpan(),
                    StartDate = b.Value == "1" ? k.Date + day.EndTime.ToTimeSpan() : k.Date + day.StartTime.ToTimeSpan()
                })))
        .Select(t =>
            t.SelectMany(current => t.Take(1),
                (current, prev) => current.StartDate - prev.EndDate > TimeSpan.FromMinutes(1)).Count(a => a) > 0
        ).Any());
Console.WriteLine(queryable.ToQueryString());