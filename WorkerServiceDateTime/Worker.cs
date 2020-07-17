using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace WorkerServiceDateTime
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.Clear();

            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _logger.LogWarning("Worker running at: {time}", DateTime.UtcNow.ToZonedDateTimeOffSet("America/Sao_Paulo").ToString("yyyy-MM-ddTHH:mm:sszzz"));

                await Task.Delay(2000, stoppingToken);
            }
        }
    }

    public static class Teste
    {
        public static DateTime ToZonedDateTime(this DateTime dateTime, string timeZoneName)
        {
            return dateTime.ToZonedDateTimeOffSet(timeZoneName).DateTime;
        }

        public static DateTimeOffset ToZonedDateTimeOffSet(this DateTime dateTime, string timeZoneName)
        {
            if (dateTime == DateTime.MinValue)
                return dateTime;

            var timeZone = DateTimeZoneProviders.Tzdb[timeZoneName];

            var instant = Instant.FromDateTimeUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
            return instant.InZone(timeZone).ToDateTimeOffset();
        }
    }
}
