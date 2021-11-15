using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Platform.IO;
using Serilog;
using Serilog.Events;
using System;

namespace Platform.Data.Doublets.Gql.Server
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().MinimumLevel.Override("Microsoft", LogEventLevel.Information).Enrich.FromLogContext().WriteTo.Console().CreateLogger();
            try
            {
                Log.Information("Starting host");
                var dbFileName = ConsoleHelpers.GetOrReadArgument(0, $"Document name (default: {Data.DefaultDatabaseFileName})", args);
                if (!string.IsNullOrWhiteSpace(dbFileName))
                {
                    Data.DefaultDatabaseFileName = dbFileName;
                }
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseSerilog().UseStartup<Startup>();
        });
    }
}
